using Microsoft.Extensions.DependencyInjection;

namespace FoundersPC.Persistence.Migrations
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMigrations(this IServiceCollection services)
        {
            // todo: add migrations

            return services;
        }
    }
}