#region Using namespaces

using FoundersPC.Identity.Application.Interfaces.Repositories.Logs;
using FoundersPC.Identity.Application.Interfaces.Repositories.Tokens;
using FoundersPC.Identity.Application.Interfaces.Repositories.Users;
using FoundersPC.Identity.Infrastructure.Contexts;
using FoundersPC.Identity.Infrastructure.Repositories.Logs;
using FoundersPC.Identity.Infrastructure.Repositories.Tokens;
using FoundersPC.Identity.Infrastructure.Repositories.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Identity.Infrastructure
{
    public static class UsersInfrastructureRegistration
    {
        public static void AddUsersRepository(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
        }

        public static void AddApiAccessTokensRepositories(this IServiceCollection services)
        {
            services.AddScoped<IApiAccessUsersTokensRepository, ApiAccessUsersTokensRepository>();
        }

        public static void AddUsersAndTokenLogsRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUsersEntrancesLogsRepository, UsersEntrancesLogsRepository>();
            services.AddScoped<IAccessTokensLogsRepository, AccessTokensLogsRepository>();
        }

        public static void AddUsersIdentityUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkUsersIdentity, UnitOfWorkUsersIdentity>();
        }

        public static void AddFoundersPCUsersContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FoundersPCUsersContext>(options =>
                                                                  options.UseSqlServer(configuration
                                                                                               .GetConnectionString("FoundersPC_Users"),
                                                                                       b =>
                                                                                               b.MigrationsAssembly(typeof(
                                                                                                                            FoundersPCUsersContext)
                                                                                                                    .Assembly
                                                                                                                    .FullName)));
        }
    }
}