#region Using namespaces

using FoundersPC.Identity.Application.Interfaces.Repositories;
using FoundersPC.Identity.Infrastructure.Contexts;
using FoundersPC.Identity.Infrastructure.Repositories;
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

        public static void AddUsersAndTokenLogsRepositories(this IServiceCollection services)
        {
            services.AddScoped<I>()
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