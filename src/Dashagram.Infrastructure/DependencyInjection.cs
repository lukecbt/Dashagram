using Amazon.Runtime;
using Amazon.S3;
using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Interfaces.Services;
using Dashagram.Infrastructure.Database;
using Dashagram.Infrastructure.Repositories;
using Dashagram.Infrastructure.Services;
using Dashagram.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Dashagram.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add PostgreSQL database
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Bind S3 settings and validate them
            services.AddOptions<S3Settings>()
                .Bind(configuration.GetSection("S3"))
                .Validate(o => 
                    !string.IsNullOrWhiteSpace(o.ServiceUrl) ||
                    !string.IsNullOrWhiteSpace(o.Region) ||
                    !string.IsNullOrWhiteSpace(o.AccessKeyId) ||
                    !string.IsNullOrWhiteSpace(o.SecretAccessKey) ||
                    !string.IsNullOrWhiteSpace(o.BucketName),
                 "S3 configuration is incomplete")
                .ValidateOnStart();

            // Add repositories
            services.AddScoped<IDogRepository, DogRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostLikeRepository, PostLikeRepository>();
            services.AddScoped<IPostCommentRepository, PostCommentRepository>();

            // Add services
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IStorageService, S3StorageService>();

            // Add Amazon S3 client
            services.AddSingleton<IAmazonS3>(s =>
            {
                var settings = s.GetRequiredService<IOptions<S3Settings>>().Value;

                return new AmazonS3Client(settings.AccessKeyId, settings.SecretAccessKey, new AmazonS3Config
                {
                    ServiceURL = settings.ServiceUrl,
                    AuthenticationRegion = settings.Region,
                    ForcePathStyle = settings.ForcePathStyle,
                    RequestChecksumCalculation = RequestChecksumCalculation.WHEN_REQUIRED,
                    ResponseChecksumValidation = ResponseChecksumValidation.WHEN_REQUIRED
                });
            });

            return services;
        }
    }
}
