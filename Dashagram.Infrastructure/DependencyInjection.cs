using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Interfaces.Services;
using Dashagram.Infrastructure.Database;
using Dashagram.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dashagram.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add PostgreSQL database
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Add repositories
            services.AddScoped<IDogRepository, DogRepository>();

            // Add services
            services.AddScoped<IIdentityService, Services.IdentityService>();

            return services;
        }
    }
}
