#region Using namespaces

using System.IO;
using FoundersPC.Web.Application;
using FoundersPC.Web.Application.Middleware;
using FoundersPC.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

#endregion

namespace FoundersPC.Web
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var cfgBuilder = new ConfigurationBuilder();
            cfgBuilder.AddConfiguration(configuration)
                      .AddJsonFile($"{Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName}\\ApplicationShared\\FoundersPC.ApplicationShared\\JwtSettings.json",
                                   false);

            Configuration = cfgBuilder.Build();
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMicroservices(Configuration);

            services.AddWebApplicationMappings();
            services.AddValidators();

            services.AddHttpClient();

            services.AddScoped<CookieCheckMiddleware>();

            services.AddCookieSecureAuthentication();

            services.AddCookieAuthorizationPolicies();

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMiddleware<CookieCheckMiddleware>();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseStatusCodePagesWithRedirects("/Error/{0}");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapDefaultControllerRoute();
                                 endpoints.MapControllers();
                             });
        }
    }
}