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

namespace FoundersPC.SharedKernel
{
    public static class ServiceExtensions
    {
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
                                                                                    .RequireRole(ApplicationRoles
                                                                                                     .Manager)
                                                                                    .Build());

                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.DefaultUserPolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                                    .RequireAuthenticatedUser()
                                                                                    .RequireRole(ApplicationRoles
                                                                                                     .DefaultUser)
                                                                                    .Build());

                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.EmployeePolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                                    .RequireAuthenticatedUser()
                                                                                    .RequireRole(ApplicationRoles.Administrator,
                                                                                                 ApplicationRoles.Manager)
                                                                                    .Build());

                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.AuthenticatedPolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                                    .RequireAuthenticatedUser()
                                                                                    .Build());
                                      });
        }

        public static IServiceCollection AddBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));

            services.AddAuthentication(options =>
                                       {
                                           options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                           options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                                           options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                       })
                    .AddJwtBearer(options =>
                                  {
                                      options.RequireHttpsMetadata = false;
                                      options.SaveToken = true;

                                      options.TokenValidationParameters = new TokenValidationParameters
                                                                          {
                                                                              ValidateIssuer = true,
                                                                              ValidIssuer = configuration["JwtConfiguration:Issuer"],

                                                                              ValidateAudience = true,
                                                                              ValidAudience = configuration["JwtConfiguration:Audience"],

                                                                              ValidateLifetime = true,

                                                                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtConfiguration:Key"])),
                                                                              ValidateIssuerSigningKey = true,

                                                                              ClockSkew = TimeSpan.Zero
                                                                          };

                                      options.Events = new JwtBearerEvents
                                                       {
                                                           OnAuthenticationFailed = context =>
                                                                                    {
                                                                                        context.NoResult();
                                                                                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                                                                        context.Response.ContentType = "application/json";

                                                                                        context.Response
                                                                                               .WriteAsync(JsonConvert.SerializeObject(new Error("error", "Token expired. Please, authorize.")))
                                                                                               .Wait();

                                                                                        return Task.CompletedTask;
                                                                                    }
                                                       };
                                  });

            return services;
        }
    }
}