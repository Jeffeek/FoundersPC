#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using FoundersPC.Application;
using FoundersPC.Application.Services;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel;
using FoundersPC.SharedKernel.ApplicationConstants;
using FoundersPC.SharedKernel.Exceptions.Filter;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Interfaces;
using FoundersPC.Web.Middleware;
using FoundersPC.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using Serilog;

#endregion

namespace FoundersPC.Web;

public sealed class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(config => config.AddSerilog(Log.Logger));
        services.AddOptions();
        services.AddBearerAuthentication(Configuration);
        services.AddAuthorizationPolicies(JwtBearerDefaults.AuthenticationScheme);
        services.AddEmailDaemon(Configuration);
        services.AddTransient<IEmailService, NullEmailService>();
        //services.AddTransient<IEmailService, EmailService>();

        services.AddApplicationOptions(Configuration);
        services.AddPipelineBehaviors(Configuration);
        services.AddApplicationServices(Configuration);
        services.AddPersistence(Configuration);
        services.AddStores();
        services.AddHttpContextAccessor();
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        //services.AddStaticData();
        services.AddSingleton<AppSettings>();

        services.AddAutoMapper((serviceProvider, autoMapper) =>
                               {
                                   autoMapper.AddCollectionMappers();
                                   autoMapper.UseEntityFrameworkCoreModel<ApplicationDbContext>(serviceProvider);
                               },
                               GetAutoMapperProfilesFromAllAssemblies()
                                   .ToArray());

        services.AddMediatR(ReflectionExtensions.GetAllAssemblies()
                                                .ToArray());

        services.AddControllersWithViews();

        services.AddControllers(options =>
                                    options.Filters.Add(new ApiExceptionFilter()))
                .AddNewtonsoftJson(options =>
                                   {
                                       options.SerializerSettings.Converters.Add(new StringEnumConverter());

                                       options.SerializerSettings.ContractResolver = new DefaultContractResolver
                                                                                     {
                                                                                         NamingStrategy = new CamelCaseNamingStrategy
                                                                                             {
                                                                                                 ProcessDictionaryKeys = false
                                                                                             }
                                                                                     };

                                       options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                                       options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                                       options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                                       options.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
#if DEBUG
                                       options.SerializerSettings.Formatting = Formatting.Indented;
#endif
                                   })
                .AddControllersAsServices()
                .AddFluentValidation(fv =>
                                     {
                                         fv.ValidatorOptions.CascadeMode = CascadeMode.Stop;
                                         fv.RegisterValidatorsFromAssemblies(ReflectionExtensions.GetAllAssemblies()
                                                                                                 .ToArray());
                                     });

        services.AddRazorPages();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<ApiTokenCheckFilter>();
        services.AddSpaStaticFiles(configuration =>
                                   {
                                       configuration.RootPath = "wwwroot";
                                   });

        services.AddOpenApiDocument(configure =>
                                    {
                                        configure.Title = "Founders PC API";

                                        configure.AddSecurity("JWT",
                                                              Enumerable.Empty<string>(),
                                                              new()
                                                              {
                                                                  Type = OpenApiSecuritySchemeType.ApiKey,
                                                                  Name = "Authorization",
                                                                  In = OpenApiSecurityApiKeyLocation.Header,
                                                                  Description = "Type into the textbox: bearer {your JWT token}."
                                                              });

                                        configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
                                    });
    }

    private static IEnumerable<Type> GetAutoMapperProfilesFromAllAssemblies() =>
        from assembly in ReflectionExtensions.GetAllAssemblies()
        from aType in assembly.GetTypes()
        where aType.IsClass && !aType.IsAbstract && aType.IsSubclassOf(typeof(Profile))
        select aType;

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseHsts();
        }

        app.Use(async (context, next) =>
                {
                    if (context.Request.Path == "/Token" || context.Request.Path == "/SignUp")
                    {
                        context.Request.ContentType = "application/x-www-form-urlencoded";
                    }
                    await next.Invoke();
                });

        app.UseCors(CorsPolicies.AllowAllPolicy);
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.UseSpaStaticFiles();

        app.UseSerilogRequestLogging();

        app.UseEndpoints(endpoints =>
                         {
                             endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                             endpoints.MapRazorPages();
                             endpoints.MapControllers();
                         });

        app.UseOpenApi();
        app.UseSwaggerUI();

        //migrationRunner.MigrateUp();
    }
}