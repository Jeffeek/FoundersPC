﻿#region Using namespaces

using FoundersPC.Application.Behaviors;
using FoundersPC.Application.Services;
using FoundersPC.Application.Services.Identity;
using FoundersPC.Application.Settings;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddPipelineBehaviors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeService, UtcDateTimeService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<PasswordEncryptorService>();

        return services;
    }

    public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<PasswordSettings>()
                .Configure<IConfiguration>((settings, cfg) => cfg.Bind(settings));

        return services;
    }

    public static IServiceCollection AddStores(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                                                               {
                                                                   options.Password.RequireDigit = true;
                                                                   options.Password.RequireLowercase = true;
                                                                   options.Password.RequireNonAlphanumeric = true;
                                                                   options.Password.RequireUppercase = true;
                                                                   options.Password.RequiredLength = 6;
                                                               })
                .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IUserStore<ApplicationUser>, UserStore>();
        services.AddScoped<IRoleStore<ApplicationRole>, RoleStore>();
        services.AddScoped<IPasswordHasher<ApplicationUser>, CustomPasswordHasher>();

        return services;
    }
}