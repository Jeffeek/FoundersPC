using FoundersPC.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoundersPC.Infrastructure.Identity
{
    public static class InfrastructureRegistration
    {
        public static void AddIdentityRepositories(this IServiceCollection services)
        {
            // TODO: implement
        }

        public static void AddIdentityUnitOfWork(this IServiceCollection services)
        {
            // TODO: implement
        }

        public static void AddFoundersPCIdentityUsersContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, FoundersPCDbIdentityUsersContext>(options =>
                                                                                   options.UseSqlServer(configuration
                                                                                                            .GetConnectionString("FoundercPC.connectionString"),
                                                                                                        b =>
                                                                                                            b.MigrationsAssembly(typeof(
                                                                                                                    FoundersPCDbIdentityUsersContext)
                                                                                                                .Assembly
                                                                                                                .FullName)));
        }
    }
}