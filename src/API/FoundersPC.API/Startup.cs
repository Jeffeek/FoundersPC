#region Using namespaces

using FoundersPC.API.Application;
using FoundersPC.API.Infrastructure;
using FoundersPC.API.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                             {
                                 options.AddPolicy("WebClientPolicy",
                                                   builder =>
                                                   {
                                                       builder.WithOrigins("http://localhost:9000")
                                                              .AllowAnyMethod()
                                                              .AllowAnyHeader()
                                                              .AllowCredentials();
                                                   });
                             });

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

            services.AddAuthentication();/*CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();*/
            services.AddAuthorization();
            //services.AddAuthorization(cfg =>
            //                          {
            //                              cfg.AddPolicy("Admin",
            //                                            builder =>
            //                                            {
            //                                                builder.RequireAuthenticatedUser();
            //                                                builder.RequireClaim("Role", "Administrator");
            //                                            });
            //                              cfg.AddPolicy("Changeable",
            //                                            builder =>
            //                                            {
            //                                                builder.RequireAuthenticatedUser();
            //                                                builder.RequireClaim("Role", "Administrator");
            //                                                builder.RequireClaim("Role", "Manager");
            //                                            });
            //                              cfg.AddPolicy("Readable",
            //                                            builder =>
            //                                            {
            //                                                builder.RequireAuthenticatedUser();
            //                                                builder.RequireClaim("Role", "DefaultUser");
            //                                                builder.RequireClaim("Role", "Administrator");
            //                                                builder.RequireClaim("Role", "Manager");
            //                                            });
            //                          });

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseCors("WebClientPolicy");

            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}