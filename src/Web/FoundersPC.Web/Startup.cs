#region Using namespaces

using System;
using FoundersPC.Web.Application;
using FoundersPC.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                               {
                                   options.LoginPath = new PathString("/Authentication/SignIn");
                                   options.AccessDeniedPath = new PathString("/Shared/Forbidden");
                                   options.LogoutPath = "/Authentication/SignIn";
                                   options.ExpireTimeSpan = TimeSpan.FromDays(30);
                               });

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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

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