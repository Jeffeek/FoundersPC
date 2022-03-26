#region Using namespaces

using System;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence.Migrations;
using FoundersPC.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Persistence;

public static class ServiceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("FoundersPCConnection"),
                                                                                           o => o.UseQuerySplittingBehavior(QuerySplittingBehavior
                                                                                               .SingleQuery))
                                                                             .EnableSensitiveDataLogging());

        services.AddScoped(p =>
                               p.GetRequiredService<IDbContextFactory<ApplicationDbContext>>()
                                .CreateDbContext());

        //services.AddDbContextFactory<ApplicationDbContext>(options => options.UseInMemoryDatabase("sojdfnjlsdf"));

        services.AddMigrations(configuration);

        return services;
    }

    public static void AddStaticData(this IServiceCollection services)
    {
        var container = services.BuildServiceProvider();
        var dbContext = container.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext();

        var role = new ApplicationRole
                   {
                       Name = "DefaultUser"
                   };

        var user = new ApplicationUser
                   {
                       ApplicationRole = role,
                       Login = "TestLogin",
                       Email = "TestLogin@mail.com",
                       EmailConfirmed = true,
                       IsBlocked = false,
                       PasswordHash = "$2b$10$UhKiRvVvwo8Pq/DxO2/ULenzI6pjBGPToAakCpynFqGVKiA6Uf5BC", //123456
                       RegistrationDate = DateTime.Now
                   };

        dbContext.Set<ApplicationRole>()
                 .Add(role);

        dbContext.Set<ApplicationUser>()
                 .Add(user);

        dbContext.SaveChanges();
    }

    public static IServiceCollection AddEmailDaemon(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<EmailBotConfiguration>()
                .Configure<IConfiguration>((bot, config) => config.Bind(bot));

        var botSettings = configuration.GetSection("EmailBotConfiguration")
                                       .Get<EmailBotConfiguration>();

        return services
               .AddFluentEmail(botSettings.MailAddress)
               .AddMailKitSender(new()
                                 {
                                     Password = botSettings.DecryptPassword(),
                                     Port = botSettings.Port,
                                     Server = botSettings.Host,
                                     UseSsl = botSettings.Ssl,
                                     RequiresAuthentication = true,
                                     User = botSettings.MailAddress
                                 })
               .Services;
    }
}