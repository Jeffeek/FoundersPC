#region Using namespaces

using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence.Migrations;
using FoundersPC.Persistence.Settings;
using FoundersPC.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;

#endregion

namespace FoundersPC.Persistence;

public static class ServiceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("FoundersPCConnection"),
        //                                                                                   o => o.UseQuerySplittingBehavior(QuerySplittingBehavior
        //                                                                                       .SingleQuery))
        //                                                                     .EnableSensitiveDataLogging());

        //services.AddScoped(p =>
        //                       p.GetRequiredService<IDbContextFactory<ApplicationDbContext>>()
        //                        .CreateDbContext());

        services.AddDbContextFactory<ApplicationDbContext>(options => options.UseInMemoryDatabase("sojdfnjlsdf"));

        services.AddMigrations(configuration);

        return services;
    }

    public static void AddStaticData(this IServiceCollection services)
    {
        var container = services.BuildServiceProvider();
        var dbContext = container.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext();

        dbContext.Set<ApplicationRole>()
                 .Add(new()
                      {
                          Name = "DefaultUser"
                      });

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