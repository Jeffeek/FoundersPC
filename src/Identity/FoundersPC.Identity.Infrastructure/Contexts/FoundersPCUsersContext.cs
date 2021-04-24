#region Using namespaces

using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Contexts
{
    public class FoundersPCUsersContext : DbContext
    {
        public FoundersPCUsersContext(DbContextOptions<FoundersPCUsersContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<AccessTokenEntity> UsersTokens { get; set; }

        public DbSet<AccessTokenLog> TokenAccessLogs { get; set; }

        public DbSet<UserEntranceLog> UsersEntrancesLogs { get; set; }
    }
}