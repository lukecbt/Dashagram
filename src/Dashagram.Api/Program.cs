using Dashagram.Api.Endpoints.Antiforgery;
using Dashagram.Api.Endpoints.Auth;
using Dashagram.Api.Endpoints.Dogs;
using Dashagram.Api.Endpoints.Posts;
using Dashagram.Api.Handlers;
using Dashagram.Application;
using Dashagram.Infrastructure;
using Dashagram.Infrastructure.Database;
using Dashagram.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add application services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add exception handling
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ExceptionHandler>();

#region Add Swagger/OpenAPI services
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configure Swagger documentation
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Dashagram API",
        Description = "An ASP.NET Core Minimal API for managing dogs in the Dashagram application.",
        Contact = new OpenApiContact
        {
            Name = "Luke",
            Email = "lukebeatty96@gmail.com"
        }
    });

    // Add JWT authentication to Swagger
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. (Authorization: Bearer [token])"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme, document)] = []
    });
});
#endregion

#region Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

builder.Services.AddAuthorization();

// Add health checks
builder.Services.AddHealthChecks()
    .AddCheck(
        name: "self-live",
        check: () => HealthCheckResult.Healthy("Application is healthy"),
        tags: ["live"]
    );
#endregion

// Antiforgery protection for image uploads
builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dashagram API v1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseExceptionHandler();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
    Predicate = p => p.Tags.Contains("live")
});

app.UseHttpsRedirection();

// Authentication and antiforgery
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

#region Register Endpoints
app.RegisterDogsEndpoints();
app.RegisterPostsEndpoints();
app.RegisterAuthEndpoints();
app.RegisterAntiforgeryEndpoints();
#endregion

app.Run();

public partial class Program { }
