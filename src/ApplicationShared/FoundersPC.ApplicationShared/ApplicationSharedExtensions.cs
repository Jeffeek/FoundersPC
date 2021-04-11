#region Using namespaces

using System;
using System.Linq;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.ApplicationShared
{
    public static class ApplicationSharedExtensions
    {
        public static void AddJwtSettings(this IServiceCollection services, IConfiguration configuration)
        {
            JwtConfiguration.Initialize(configuration);
            services.AddSingleton(JwtConfiguration.Configuration);
        }

        public static void AddBearerAuthenticationWithSettings(this IServiceCollection services)
        {
            var jwtConfigService = services.FirstOrDefault(x => x.ServiceType == typeof(JwtConfiguration));

            if (jwtConfigService is null) throw new Exception();

            if (!(jwtConfigService.ImplementationInstance is JwtConfiguration service))
                throw new Exception("Bearer settings middleware not found");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                                  config =>
                                  {
                                      var key = service.GetSymmetricSecurityKey();
                                      config.BackchannelTimeout = TimeSpan.FromSeconds(20);

                                      config.TokenValidationParameters = new TokenValidationParameters
                                                                         {
                                                                             ValidateAudience = false,
                                                                             ValidIssuer = service.Issuer,
                                                                             ValidAudience = service.Audience,
                                                                             IssuerSigningKey = key
                                                                         };
                                  });
        }

        public static void AddAuthorizationPolicies(this IServiceCollection services, string scheme)
        {
            services.AddAuthorization(configuration =>
                                      {
                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.AdministratorPolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                      .RequireAuthenticatedUser()
                                                                      .RequireRole(ApplicationRoles
                                                                          .Administrator)
                                                                      .Build());

                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.ManagerPolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                      .RequireAuthenticatedUser()
                                                                      .RequireRole(ApplicationRoles.Manager)
                                                                      .Build());

                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.DefaultUserPolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                      .RequireAuthenticatedUser()
                                                                      .RequireRole(ApplicationRoles.DefaultUser)
                                                                      .Build());

                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.EmployeePolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                      .RequireAuthenticatedUser()
                                                                      .RequireRole(ApplicationRoles
                                                                              .Administrator,
                                                                          ApplicationRoles.Manager)
                                                                      .Build());

                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.AuthenticatedPolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                      .RequireAuthenticatedUser()
                                                                      .Build());
                                      });
        }
    }
}