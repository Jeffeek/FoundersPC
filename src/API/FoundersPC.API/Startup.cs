#region Using namespaces

using System.IO;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Middleware;
using FoundersPC.API.Infrastructure;
using FoundersPC.API.Services;
using FoundersPC.ApplicationShared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

#endregion

namespace FoundersPC.API
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();

            builder
                .AddJsonFile($"{Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName}\\ApplicationShared\\FoundersPC.ApplicationShared\\Jwt\\JwtSettings.json",
                             false)
                .AddConfiguration(configuration, false);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(config => config.AddSerilog(Log.Logger));

            services.AddControllers();

            //
            services.AddHardwareRepositories();

            //
            services.AddHardwareUnitOfWork();

            //
            services.AddFoundersPCHardwareContext(Configuration);

            //
            services.AddHardwareServices();

            //
            services.AddHardwareApplicationExtensions();

            //
            services.AddValidators();

            services.AddJwtSettings(Configuration);
            services.AddBearerAuthenticationWithSettings();

            services.AddBearerAuthorizationPolicies();

            services.AddApiVersioning(options =>
                                      {
                                          options.AssumeDefaultVersionWhenUnspecified = true;
                                          options.DefaultApiVersion = new ApiVersion(1, 0);
                                          options.ReportApiVersions = true;
                                      });

            services.AddSwaggerGen(options => options.SwaggerDoc("v1",
                                                                 new OpenApiInfo
                                                                 {
                                                                     Title = "FoundersPC.API",
                                                                     Version = "v1.0"
                                                                 }));

            services.AddScoped<AccessTokenValidatorMiddleware>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json",
                                                                    "FoundersPC.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<AccessTokenValidatorMiddleware>();
            //app.UseCors("WebClientPolicy");

            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}