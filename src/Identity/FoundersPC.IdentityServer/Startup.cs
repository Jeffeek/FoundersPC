#region Using namespaces

using System.IO;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application;
using FoundersPC.Identity.Infrastructure;
using FoundersPC.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <exception cref="T:System.OverflowException"><paramref name="configuration[JwtSettings:HoursToExpire]" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="configuration[JwtSettings:HoursToExpire]" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.NotSupportedException">Inner JWT configuration was null.</exception>
        /// <exception cref="T:System.FormatException"><paramref name="configuration[JwtSettings:HoursToExpire]" /> is not in the correct format.</exception>
        /// <exception cref="T:System.Data.NoNullAllowedException">Condition.</exception>
        /// <exception cref="T:System.TypeInitializationException">Bearer settings middleware not found</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (for more information, see Character Encoding in .NET)
        ///  -and-
        ///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
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
                                                             .WithOrigins("https://localhost:9000")
                                                             .AllowAnyMethod()
                                                             .Build());

                                 options.AddPolicy(ApplicationCorsPolicies.TokenCheckPolicy,
                                                   config =>
                                                       config.WithOrigins(MicroservicesUrls.APIServer)
                                                             .WithMethods("GET")
                                                             .WithHeaders("HARDWARE-ACCESS-TOKEN")
                                                             .Build());
                             });

            services.AddSwaggerGen(c => c.SwaggerDoc("v1",
                                                     new OpenApiInfo
                                                     {
                                                         Title = "FoundersPC.IdentityServer",
                                                         Version = "v1"
                                                     }));
        }

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

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}