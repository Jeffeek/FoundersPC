#region Using namespaces

using System.IO;
using System.Text.Json;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.Middleware;
using FoundersPC.Identity.Application;
using FoundersPC.Identity.Infrastructure;
using FoundersPC.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

#endregion

namespace FoundersPC.IdentityServer
{
    public class Startup
    {
        #region Docs

        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The operating system is Windows CE, which does not have current directory functionality.
        ///     This method is available in the .NET Compact Framework, but is not currently supported.
        /// </exception>
        /// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path"/> is read-only.</exception>
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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined
        ///     maximum length. For more information, see the <see cref="T:System.IO.PathTooLongException"/> topic.
        /// </exception>

        #endregion

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();

            builder
                .AddJsonFile($"{Directory.GetCurrentDirectory()}\\EmailBotConfiguration.json",
                             false,
                             true)
                .AddJsonFile($"{Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName}\\ApplicationShared\\FoundersPC.ApplicationShared\\Jwt\\JwtSettings.json",
                             false)
                .AddConfiguration(configuration, false);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        #region Docs

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <exception cref="T:System.OverflowException">
        ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> represents a
        ///     number less than <see cref="F:System.Int32.MinValue"/> or greater than <see cref="F:System.Int32.MaxValue"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> is
        ///     <see langword="null"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">Inner JWT configuration was null.</exception>
        /// <exception cref="T:System.FormatException">
        ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> is not in the
        ///     correct format.
        /// </exception>
        /// <exception cref="T:System.Data.NoNullAllowedException">Condition.</exception>
        /// <exception cref="T:System.TypeInitializationException">Bearer settings middleware not found</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">
        ///     A fallback occurred (for more information, see Character Encoding in .NET)
        ///     -and-
        ///     <see cref="P:System.Text.Encoding.EncoderFallback"/> is set to <see cref="T:System.Text.EncoderExceptionFallback"/>
        ///     .
        /// </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="value"/> is equal to <see cref="F:System.Double.NaN"/>.</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Password:Key</exception>

        #endregion

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(config => config.AddSerilog(Log.Logger));

            services.AddBotEmailConfigurationAndService(Configuration);

            services.AddJwtSettings(Configuration);
            services.AddBearerAuthenticationWithSettings();

            services.AddFoundersPCUsersContext(Configuration);

            services.AddUsersRepository();
            services.AddApiAccessTokensRepositories();
            services.AddUsersAndTokenLogsRepositories();

            services.AddUsersIdentityUnitOfWork();

            services.AddEncryptionServices();
            services.AddLogsServices();
            services.AddAccessTokensServices();
            services.AddUsersIdentityServices();

            services.AddMappings();
            services.AddValidators();

            services.AddAuthorizationPolicies(JwtBearerDefaults.AuthenticationScheme);

            services.AddControllers();

            services.AddCors(options =>
                             {
                                 options.AddPolicy(ApplicationCorsPolicies.WebClientPolicy,
                                                   config =>
                                                       config.AllowCredentials()
                                                             .WithOrigins(MicroservicesUrls.WebServer)
                                                             .AllowAnyMethod()
                                                             .Build());

                                 options.AddPolicy(ApplicationCorsPolicies.TokenCheckPolicy,
                                                   config =>
                                                       config.WithOrigins(MicroservicesUrls.APIServer)
                                                             .WithMethods("GET")
                                                             .WithHeaders("HARDWARE-ACCESS-TOKEN")
                                                             .Build());
                             });

            services.AddScoped<ModelValidationAttribute>();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1",
                                                     new OpenApiInfo
                                                     {
                                                         Title = "FoundersPC.IdentityServer",
                                                         Version = "v1"
                                                     }));
        }

        #region Docs

        /// <exception cref="T:System.NotSupportedException">
        ///     There is no compatible
        ///     <see cref="System.Text.Json.Serialization.JsonConverter"/> for <typeparamref name="TValue"/> or its serializable
        ///     members.
        /// </exception>

        #endregion

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoundersPC.IdentityServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("WebPolicy");
            app.UseCors("TokenCheckPolicy");

            app.UseExceptionHandler(config =>
                                    {
                                        config.Run(async context =>
                                                   {
                                                       context.Response.StatusCode = 500;
                                                       context.Response.ContentType = "application/json";
                                                       var error = context.Features.Get<IExceptionHandlerFeature>();

                                                       if (error != null)
                                                       {
                                                           var ex = error.Error;
                                                           context.Response.StatusCode = 500;
                                                           await context.Response.WriteAsync(JsonSerializer.Serialize(ex));
                                                           await context.Response.CompleteAsync();
                                                       }
                                                   });
                                    });

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}