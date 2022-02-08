using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FluentMigrator.Runner;
using FoundersPC.Application;
using FoundersPC.Application.Services;
using FoundersPC.Application.Services.Identity;
using FoundersPC.Application.Settings;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Interfaces;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Services;
using FoundersPC.UI.Admin.Views;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Prism.Microsoft.DependencyInjection;
using Serilog;

namespace FoundersPC.UI.Admin;

public partial class App
{
    protected override IContainerExtension CreateContainerExtension() => PrismContainerExtension.Current;

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        Log.Logger = new LoggerConfiguration()
                     .MinimumLevel.Debug()
                     .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "logs", $"log_{DateTime.Now:dd-MM-yyyy}.txt"))
                     .CreateLogger();

        SetupExceptionHandling();

        PrismContainerExtension.Current.RegisterServices(services =>
                                                         {
                                                             services.AddLogging(x => x.AddSerilog(Log.Logger));
                                                             services.AddOptions();
                                                             AddConfiguration(services);

                                                             var configuration = services.BuildServiceProvider()
                                                                                         .GetService<IConfiguration>()!;

                                                             services.Configure<PasswordSettings>(configuration.GetSection("PasswordSettings"));
                                                             var titlebarLocator = new TitleBarLocator();
                                                             var mainWindowTitleBarLocator = new MainWindowTitleBarLocator();
                                                             var selectedObjectLocator = new SelectedObjectLocator();
                                                             services.AddSingleton(titlebarLocator);
                                                             services.AddSingleton(mainWindowTitleBarLocator);
                                                             services.AddSingleton(selectedObjectLocator);
                                                             services.AddApplicationServices(configuration);
                                                             services.AddScoped<IEmailService, NullEmailService>();
                                                             //services.AddScoped<IEmailService, EmailService>();
                                                             services.AddEmailDaemon(configuration);
                                                             services.AddScoped<PasswordEncryptorService>();
                                                             services.AddScoped<IPasswordHasher<ApplicationUser>, CustomPasswordHasher>();
                                                             services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                                                             {
                                                                 options.Password.RequireDigit = false;
                                                                 options.Password.RequireLowercase = false;
                                                                 options.Password.RequireNonAlphanumeric = false;
                                                                 options.Password.RequireUppercase = false;
                                                                 options.Password.RequiredLength = 6;
                                                             });
                                                             services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
                                                             services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
                                                             services.AddPipelineBehaviors(configuration);
                                                             services.AddApplicationOptions(configuration);
                                                             services.AddPersistence(configuration);
                                                             services.AddTransient<FilterOptions>();
                                                             AddMediator(services);
                                                             AddAutoMapper(services);

                                                             var currentUserService = new CurrentUserService();
                                                             services.AddSingleton<ICurrentUserService, CurrentUserService>(_ => currentUserService);
                                                             var container = services.BuildServiceProvider();
                                                             var migrationRunner = container.GetRequiredService<IMigrationRunner>();
                                                             migrationRunner.MigrateUp();
                                                             var metadata = RegisterMetadataLocator(container);
                                                             services.AddSingleton(metadata);
                                                         });
    }

    private static MetadataPackageLocator RegisterMetadataLocator(IServiceProvider serviceProvider)
    {
        var metadataPackageLocator = new MetadataPackageLocator(serviceProvider.GetRequiredService<IMediator>(), serviceProvider.GetRequiredService<IMapper>());

        return metadataPackageLocator;
    }

    protected override Window CreateShell()
    {
        var window = Container.Resolve<MainWindow>();
        window.Focus();
        window.Activate();

        return window;
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Log.CloseAndFlush();
        base.OnExit(e);
    }

    private void SetupExceptionHandling()
    {
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
                                                          LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

        DispatcherUnhandledException += (_, e) =>
                                        {
                                            LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
                                            e.Handled = true;
                                        };

        TaskScheduler.UnobservedTaskException += (_, e) =>
                                                 {
                                                     LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
                                                     e.SetObserved();
                                                 };
    }

    private static void LogUnhandledException(Exception exception, string source)
    {
        var message = $"Unhandled exception ({source})";

        try
        {
            var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            message = $"Unhandled exception in {assemblyName.Name} v{assemblyName.Version}";
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Exception in LogUnhandledException");
        }
        finally
        {
            Log.Logger.Error(exception, message);
        }
    }

    private static void AddConfiguration(IServiceCollection services) =>
        services.AddScoped<IConfiguration>(_ =>
                                           {
                                               var builder = new ConfigurationBuilder()
                                                             .SetBasePath(Directory.GetCurrentDirectory())
                                                             .AddJsonFile("appsettings.json");

                                               return builder.Build();
                                           });

    private static void AddMediator(IServiceCollection services) =>
        services.AddMediatR(ReflectionExtensions.GetAllAssemblies()
                                                .ToArray());

    private static void AddAutoMapper(IServiceCollection services) =>
        services.AddAutoMapper((serviceProvider, autoMapper) =>
                               {
                                   autoMapper.AddCollectionMappers();
                                   autoMapper.UseEntityFrameworkCoreModel<ApplicationDbContext>(serviceProvider);
                               },
                               GetTypes(),
                               ServiceLifetime.Singleton);

    private static IEnumerable<Type> GetTypes()
    {
        var res = ReflectionExtensions.GetAllAssemblies()
                                      .SelectMany(assembly => assembly.GetTypes(),
                                                  (assembly, aType) => new
                                                                       {
                                                                           assembly,
                                                                           aType
                                                                       })
                                      .Where(t => t.aType.IsClass
                                                  && !t.aType.IsAbstract
                                                  && t.aType.IsSubclassOf(typeof(Profile)))
                                      .Select(t => t.aType)
                                      .DistinctBy(x => x.FullName ?? x.Name)
                                      .ToList();

        return res;
    }
}