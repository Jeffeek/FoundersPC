#region Using namespaces

using FoundersPC.Application.Interfaces.Repositories.Users;
using FoundersPC.Infrastructure.Identity.Contexts;
using FoundersPC.Infrastructure.Identity.Repositories;
using FoundersPC.Infrastructure.Identity.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Infrastructure.Identity
{
    public static class InfrastructureRegistration
    {
        public static void AddIdentityRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepositoryAsync, UsersRepositoryAsync>();
            services.AddScoped<IRolesRepositoryAsync, RolesRepositoryAsync>();
        }

        public static void AddIdentityUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkUsersIdentityAsync, UnitOfWorkUsersIdentityAsync>();
        }

        public static void AddFoundersPCIdentityUsersContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FoundersPCDbIdentityUsersContext>(options =>
                                                                        options.UseSqlServer(configuration
                                                                                                 .GetConnectionString("FoundercPC.connectionString"),
                                                                                             b =>
                                                                                                 b.MigrationsAssembly(typeof(
                                                                                                         FoundersPCDbIdentityUsersContext
                                                                                                     )
                                                                                                     .Assembly
                                                                                                     .FullName)));
        }
    }
}