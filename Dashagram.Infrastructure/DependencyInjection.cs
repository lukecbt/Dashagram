using Dashagram.Application.Common.Interfaces;
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
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Add repositories
            services.AddScoped<IDogRepository, DogRepository>();

            return services;
        }
    }
}
