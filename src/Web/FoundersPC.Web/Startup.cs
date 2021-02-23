#region Using namespaces

using System;
using FoundersPC.Web.Domain.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TokenConfiguration = FoundersPC.AuthenticationShared.TokenConfiguration;

#endregion

namespace FoundersPC.Web
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                               {
                                   options.LoginPath = new PathString("/Auth/Login");
                                   options.AccessDeniedPath = new PathString("/Auth/Login");
                               });

            services.AddSession();

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

            app.UseSession(new SessionOptions()
                           {
                               Cookie = new CookieBuilder()
                                        {
                                            SecurePolicy = CookieSecurePolicy.Always,
                                            Expiration = TimeSpan.FromHours(24),
                                            IsEssential = true,
                                            HttpOnly = false,
                                            Name = "UserCredentials"
                                        },
                               IdleTimeout = TimeSpan.FromSeconds(20),
                               IOTimeout = TimeSpan.FromSeconds(5)
                           });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}