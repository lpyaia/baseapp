using BaseApp.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BaseApp.Infrastructure.Identity.Authentication
{
    public static class JwtSecurityExtension
    {
        public static IServiceCollection AddJwtSecurity(this IServiceCollection services, TokenConfiguration tokenConfig)
        {
            services.AddSingleton(tokenConfig);

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BaseAppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<AccessManager>();

            var signingConfiguration = new SigningConfiguration(tokenConfig);
            services.AddSingleton(signingConfiguration);

            services.AddTransient<IdentityDbInitializer>();

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.IssuerSigningKey = signingConfiguration.Key;
                paramsValidation.ValidAudience = tokenConfig.Audience;
                paramsValidation.ValidIssuer = tokenConfig.Issuer;

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(options =>
            {
                var authorizationPolicyBuild = new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();

                options.AddPolicy("Bearer", authorizationPolicyBuild);
            });

            return services;
        }
    }
}