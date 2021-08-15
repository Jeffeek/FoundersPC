using FluentEmail.MailKitSmtp;
using FoundersPC.Persistence.Migrations;
using FoundersPC.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoundersPC.Persistence
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                                                               o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery))
                                                                                 .EnableSensitiveDataLogging(true));

            services.AddScoped<ApplicationDbContext>(p =>
                                                         p.GetRequiredService<IDbContextFactory<ApplicationDbContext>>()
                                                          .CreateDbContext());

            services.AddMigrations(configuration);

            return services;
        }

        public static IServiceCollection AddEmailDaemon(this IServiceCollection services, IConfiguration configuration)
        {
            var botSettings = configuration.GetSection("EmailDaemonConfiguration");

            var cfg = new EmailBotConfiguration(botSettings);

            return services
                   .AddFluentEmail("fromemail@test.test")
                   .AddMailKitSender(new SmtpClientOptions()
                                     {
                                         Password = cfg.Password,
                                         Port = cfg.Port,
                                         Server = cfg.Host,
                                         UseSsl = true,
                                         RequiresAuthentication = true,
                                         User = cfg.MailAddress
                                     })
                   .Services;
        }
    }
}