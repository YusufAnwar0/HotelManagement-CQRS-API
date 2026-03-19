using Application.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.MiddleWares;
using Presentation.Services;
using System.Text;

namespace Presentation.Helpers
{
    public static class PresentationDependencies
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddOpenApi();

            var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>()!;
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };
                });

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
