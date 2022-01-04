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
using FoundersPC.Persistence;
using FoundersPC.SharedKernel;
using FoundersPC.SharedKernel.Exceptions.Filter;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Interfaces;
using FoundersPC.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
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
    public Startup(IConfiguration configuration) => Configuration = configuration;

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(config => config.AddSerilog(Log.Logger));
        services.AddAuthorizationPolicies(JwtBearerDefaults.AuthenticationScheme);
        services.AddOptions();
        services.AddEmailDaemon(Configuration);

        services.AddCors(options =>
                         {
                             options.AddPolicy("AllowAllPolicy",
                                               builder =>
                                               {
                                                   builder
                                                       .AllowAnyOrigin()
                                                       .AllowAnyMethod()
                                                       .AllowAnyHeader();
                                               });
                         });

        services.AddApplicationOptions(Configuration);
        services.AddPipelineBehaviors(Configuration);
        services.AddApplicationServices(Configuration);
        services.AddPersistence(Configuration);
        services.AddStores();
        services.AddBearerAuthentication(Configuration);
        services.AddHttpContextAccessor();
        services.AddTransient<ICurrentUserService, CurrentUserService>();

        services.AddAutoMapper((serviceProvider, autoMapper) =>
                               {
                                   autoMapper.AddCollectionMappers();
                                   autoMapper.UseEntityFrameworkCoreModel<ApplicationDbContext>(serviceProvider);
                               },
                               GetAutoMapperProfilesFromAllAssemblies()
                                   .ToArray());

        services.AddMediatR(ReflectionExtensions.GetAllAssemblies()
                                                .ToArray());

        services.AddHttpClient();

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
        services.AddControllersWithViews();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

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

        app.UseExceptionHandler(config => config.Run(context =>
                                                     {
                                                         var statusCode = 400;
                                                         var error = context.Features.Get<IExceptionHandlerFeature>();

                                                         if (error != null)
                                                             statusCode = 500;

                                                         context.Response.StatusCode = statusCode;

                                                         return context.Response.CompleteAsync();
                                                     }));

        app.UseCors("AllowAllPolicy");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
                         {
                             endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                             endpoints.MapRazorPages();
                             endpoints.MapControllers();
                         });

        app.UseSwaggerUI();
        app.UseOpenApi();
        migrationRunner.MigrateUp();
    }
}