#region Using namespaces

using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.SharedKernel.ApplicationConstants;
using FoundersPC.SharedKernel.Jwt;
using FoundersPC.SharedKernel.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

#endregion

namespace FoundersPC.SharedKernel;

public static class ServiceExtensions
{
    public static void AddAuthorizationPolicies(this IServiceCollection services, string scheme)
    {
        services.AddCors();

        services.AddAuthorization(configuration =>
                                  {
                                      configuration.AddPolicy(ApplicationAuthorizationPolicies.AdministratorPolicy,
                                                              builder => builder.AddAuthenticationSchemes(scheme)
                                                                                .RequireAuthenticatedUser()
                                                                                .RequireRole(ApplicationRoles.Administrator)
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
                                                                                .RequireRole(ApplicationRoles.Administrator, ApplicationRoles.Manager)
                                                                                .Build());

                                      configuration.AddPolicy(ApplicationAuthorizationPolicies.AuthenticatedPolicy,
                                                              builder => builder.AddAuthenticationSchemes(scheme)
                                                                                .RequireAuthenticatedUser()
                                                                                .Build());
                                  });
    }

    public static IServiceCollection AddBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("JwtConfiguration");

        services.AddOptions<JwtConfiguration>()
                .Configure<IConfiguration>((opt, cfg) => cfg.GetSection("JwtConfiguration")
                                                            .Bind(opt));

        services.Configure<JwtConfiguration>(jwtSection);

        services.AddAuthentication(options =>
                                   {
                                       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                       options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                                       options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                   })
                .AddJwtBearer(options =>
                              {
                                  var jwt = jwtSection.Get<JwtConfiguration>();

                                  options.RequireHttpsMetadata = false;
                                  options.SaveToken = true;

                                  options.TokenValidationParameters = new()
                                                                      {
                                                                          ValidateIssuer = true,
                                                                          ValidIssuer = jwt.Issuer,

                                                                          ValidateAudience = true,
                                                                          ValidAudience = jwt.Audience,

                                                                          ValidateLifetime = true,

                                                                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.Key)),
                                                                          ValidateIssuerSigningKey = true,

                                                                          ClockSkew = TimeSpan.Zero
                                                                      };

                                  options.Events = new()
                                                   {
                                                       OnAuthenticationFailed = async context =>
                                                                                {
                                                                                    context.NoResult();
                                                                                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                                                                    context.Response.ContentType = "application/json";
                                                                                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new Error("error", "Token expired. Please, authorize.")));
                                                                                }
                                                   };
                              });

        return services;
    }
}