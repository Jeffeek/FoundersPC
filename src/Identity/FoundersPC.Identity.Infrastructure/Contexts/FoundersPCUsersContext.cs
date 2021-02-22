#region Using namespaces

using FoundersPC.Identity.Domain.Entities;
using FoundersPC.Identity.Domain.Entities.Tokens;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Contexts
{
    public class FoundersPCUsersContext : DbContext
    {
        public FoundersPCUsersContext(DbContextOptions<FoundersPCUsersContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<ApiAccessToken> ApiTokens { get; set; }

        public DbSet<ApiAccessUserToken> UsersTokens { get; set; }

        public DbSet<AccessTokenLog> TokenAccessLogs { get; set; }
    }
}