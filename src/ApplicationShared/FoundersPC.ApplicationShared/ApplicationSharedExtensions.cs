#region Usings

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.ApplicationShared
{
    public static class ApplicationSharedExtensions
    {
        public static void AddJwtSettings(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder();
            var config = builder.AddJsonFile($"{Directory.GetCurrentDirectory()}\\jwtSettings.json").Build();

            services.AddSingleton(new JwtConfiguration(config));
        }

        public static void AddBearerAuthenticationWithSettings(this IServiceCollection services)
        {
            var jwtConfigService = services.FirstOrDefault(x => x.ServiceType == typeof(JwtConfiguration));

            if (jwtConfigService is null) throw new Exception();

            if (!(jwtConfigService.ImplementationInstance is JwtConfiguration service)) throw new Exception();

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
    }
}