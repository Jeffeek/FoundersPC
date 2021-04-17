#region Using namespaces

using System.IO;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Application;
using FoundersPC.Web.Application.Middleware;
using FoundersPC.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
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
        /// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path"/> is read-only.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path was not found.</exception>
        /// <exception cref="T:System.Security.SecurityException">
        ///     .NET Framework only: The caller does not have the required
        ///     permissions.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="path"/> is a zero-length string, contains only white
        ///     space, or contains one or more invalid characters. You can query for invalid characters with the
        ///     <see cref="M:System.IO.Path.GetInvalidPathChars"/> method.
        /// </exception>
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined
        ///     maximum length. For more information, see the <see cref="T:System.IO.PathTooLongException"/> topic.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
        public Startup(IConfiguration configuration)
        {
            var cfgBuilder = new ConfigurationBuilder();

            cfgBuilder.AddConfiguration(configuration)
                      .AddJsonFile($"{Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName}\\ApplicationShared\\FoundersPC.ApplicationShared\\Jwt\\JwtSettings.json",
                                   false);

            Configuration = cfgBuilder.Build();
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(config => config.AddSerilog(Log.Logger));

            services.AddMicroservices(Configuration);

            services.AddWebApplicationMappings();
            services.AddValidators();

            services.AddHttpClient();

            services.AddScoped<CookieCheckMiddleware>();

            services.AddCookieSecureAuthentication();

            services.AddAuthorizationPolicies(CookieAuthenticationDefaults.AuthenticationScheme);

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

            app.UseExceptionHandler(config =>
                                    {
                                        config.Run(async context =>
                                                   {
                                                       var statusCode = 400;
                                                       var error = context.Features.Get<IExceptionHandlerFeature>();

                                                       if (error != null)
                                                           statusCode = 500;

                                                       context.Response.StatusCode = statusCode;
                                                       context.Response.Redirect($"/Error/{statusCode}");
                                                       await context.Response.CompleteAsync();
                                                   });
                                    });

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapDefaultControllerRoute();
                                 endpoints.MapControllers();
                             });
        }
    }
}