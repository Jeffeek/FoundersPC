#region Using namespaces

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
        public Startup(IConfiguration configuration) => Configuration = configuration;

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMicroservices(Configuration);

            services.AddWebApplicationMappings();
            services.AddValidators();

            services.AddHttpClient();

            services.AddScoped<CookieCheckMiddleware>();

            services.AddCookieSecureAuthentication();

            services.AddAuthorization();

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMiddleware<CookieCheckMiddleware>();

            app.UseSerilogRequestLogging();

            //app.UseExceptionHandler(new ExceptionHandlerOptions()
            //                        {
            //                            AllowStatusCode404Response = true,
            //                            ExceptionHandlingPath = new PathString("Error/404"),
            //                        });

            app.UseRouting();

            app.UseStatusCodePagesWithRedirects("/Error/{0}");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllerRoute("default",
                                                              "{controller=Home}/{action=Index}");

                                 endpoints.MapDefaultControllerRoute();
                             });
        }
    }
}