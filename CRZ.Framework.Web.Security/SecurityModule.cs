using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace CRZ.Framework.Web.Security
{
    public class SecurityModule
    {
        public SecurityModule(IServiceCollection services, SigninConfiguration signinConfiguration, TokenConfiguration tokenConfiguration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (signinConfiguration == null) throw new ArgumentNullException(nameof(signinConfiguration));
            if (tokenConfiguration == null) throw new ArgumentNullException(nameof(tokenConfiguration));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var paramsValidation = options.TokenValidationParameters;

                // configurations
                paramsValidation.IssuerSigningKey = signinConfiguration.Key;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;

                // validations
                paramsValidation.ValidateAudience = true;
                paramsValidation.ValidateIssuer = true;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;

                // set tolerance time for token expiration
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
