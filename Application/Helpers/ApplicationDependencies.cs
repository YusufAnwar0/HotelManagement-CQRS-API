using Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Helpers
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(cfg => { }, assembly);

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));

                config.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
