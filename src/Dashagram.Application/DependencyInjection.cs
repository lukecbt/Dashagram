using Dashagram.Application.Common.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dashagram.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Add MediatR for CQRS pattern
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });
            return services;
        }
    }
}
