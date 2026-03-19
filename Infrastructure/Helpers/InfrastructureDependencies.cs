using Application.Interface;
using Domain.IRepositories;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Repositories;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Infrastructure.Helpers
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<AuditInterceptor>();

            services.AddDbContext<Context>((sp, options) =>
            {
                var auditInterceptor = sp.GetRequiredService<AuditInterceptor>();

                options.UseSqlServer(configuration.GetConnectionString("conn"))
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                       //.LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                       .AddInterceptors(auditInterceptor);
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "Hotel";
            });

            services.AddScoped<IJwtProvider, JwtProvider>(); 
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ITransactionalContext, TransactionalContext>();

            return services;
        }
    }
}
