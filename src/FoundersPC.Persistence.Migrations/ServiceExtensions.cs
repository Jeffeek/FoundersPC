using FluentMigrator.Runner;
using FoundersPC.Persistence.Migrations.Migrations._2021._08._15;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoundersPC.Persistence.Migrations
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                                       .AddSqlServer()
                                       .WithGlobalConnectionString(configuration.GetConnectionString("FoundersPCConnection"))
                                       .ScanIn(typeof(AddRolesAndUsersTable).Assembly)
                                       .For.Migrations()
                                       .For.EmbeddedResources())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            return services;
        }
    }
}