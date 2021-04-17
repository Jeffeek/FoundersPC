#region Using namespaces

using System.IO;
using System.Text.Json;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Middleware;
using FoundersPC.API.Infrastructure;
using FoundersPC.API.Services;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        #region Docs

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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined
        ///     maximum length. For more information, see the <see cref="T:System.IO.PathTooLongException"/> topic.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format.</exception>

        #endregion

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

        #region Docs

        /// <exception cref="T:System.OverflowException">
        ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> represents a
        ///     number less than <see cref="F:System.Int32.MinValue"/> or greater than <see cref="F:System.Int32.MaxValue"/>.
        /// </exception>
        /// <exception cref="T:System.Data.NoNullAllowedException">Condition.</exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> is
        ///     <see langword="null"/>.
        /// </exception>
        /// <exception cref="T:System.TypeInitializationException">Bearer settings middleware not found</exception>
        /// <exception cref="T:System.NotSupportedException">Inner JWT configuration was null.</exception>
        /// <exception cref="T:System.FormatException">
        ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> is not in the
        ///     correct format.
        /// </exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">
        ///     A fallback occurred (for more information, see Character Encoding in .NET)
        ///     -and-
        ///     <see cref="P:System.Text.Encoding.EncoderFallback"/> is set to <see cref="T:System.Text.EncoderExceptionFallback"/>
        ///     .
        /// </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="value"/> is equal to <see cref="F:System.Double.NaN"/>.</exception>

        #endregion

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

            services.AddAuthorizationPolicies(JwtBearerDefaults.AuthenticationScheme);

            services.AddApiVersioning(options =>
                                      {
                                          options.AssumeDefaultVersionWhenUnspecified = true;
                                          options.DefaultApiVersion = new ApiVersion(1, 0);
                                          options.ReportApiVersions = true;
                                      });

            // todo: убрать сваггер
            services.AddSwaggerGen(options => options.SwaggerDoc("v1",
                                                                 new OpenApiInfo
                                                                 {
                                                                     Title = "FoundersPC.API",
                                                                     Version = "v1.0"
                                                                 }));

            services.AddScoped<AccessTokenValidatorMiddleware>();
            services.AddScoped<ModelValidationAttribute>();
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

                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json",
                                                                    "FoundersPC.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSerilogRequestLogging();

            app.UseMiddleware<AccessTokenValidatorMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

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
                                                           await context.Response.WriteAsync(JsonSerializer.Serialize(ex));
                                                       }
                                                   });
                                    });

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}