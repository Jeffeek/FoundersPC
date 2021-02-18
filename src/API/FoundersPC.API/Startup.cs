#region Using namespaces

using FoundersPC.Application;
using FoundersPC.Infrastructure.API;
using FoundersPC.Infrastructure.Identity;
using FoundersPC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            //.AddNewtonsoftJson(setup => setup.SerializerSettings.ContractResolver =
            //                             new CamelCasePropertyNamesContractResolver());

            services.AddControllers();
            //
            services.AddHardwareRepositories();
            services.AddIdentityRepositories();
            //
            services.AddHardwareUnitOfWork();
            services.AddIdentityUnitOfWork();
            //
            services.AddFoundersPCHardwareContext(Configuration);
            services.AddFoundersPCIdentityUsersContext(Configuration);
            //
            services.AddHardwareServices();
            services.AddUserServices(Configuration);
            //
            services.AddApplicationExtensions();

            // TODO: authorization
            services.AddAuthorization(options => options.AddPolicy("AdministratorsOnly",
                                                                   policy =>
                                                                       policy.RequireRole("Administrator")));

            // TODO: authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

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
                                                                     Version = "v1"
                                                                 }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "FoundersPC.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}