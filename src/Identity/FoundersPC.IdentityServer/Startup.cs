#region Using namespaces

using System.IO;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application;
using FoundersPC.Identity.Infrastructure;
using FoundersPC.Identity.Services;
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(config => config.AddSerilog(Log.Logger));

            services.AddBotEmailConfigurationAndService(Configuration);

            services.AddControllers();

            services.AddUsersRepository();
            services.AddApiAccessTokensRepositories();
            services.AddUsersAndTokenLogsRepositories();

            services.AddUsersIdentityUnitOfWork();

            services.AddFoundersPCUsersContext(Configuration);

            services.AddEncryptionServices();
            services.AddLogsServices();
            services.AddTokenServices();
            services.AddUsersIdentityServices();

            services.AddMappings();
            services.AddValidators();

            services.AddJwtSettings(Configuration);
            services.AddBearerAuthenticationWithSettings();

            services.AddBearerAuthorizationPolicies();

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

            //app.UseCors("WebClientPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}