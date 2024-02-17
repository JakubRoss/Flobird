using Domain.Authentication;
using Domain.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Domain.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddDomain(this IServiceCollection service, IConfiguration configuration)
        {
            #region Authetictaion
            var authenticationSettings = new AuthenticationSettings();
            configuration.GetSection("Authentication").Bind(authenticationSettings);
            service.AddSingleton(authenticationSettings);
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";

                options.DefaultScheme = "Bearer";

                options.DefaultChallengeScheme = "Bearer";

            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = true;

                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidIssuer = authenticationSettings.JwtIssuer,

                    ValidAudience = authenticationSettings.JwtIssuer,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });
            #endregion

            service.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            service.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}
