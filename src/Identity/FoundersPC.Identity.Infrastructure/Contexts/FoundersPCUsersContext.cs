#region Using namespaces

using FoundersPC.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Contexts
{
    public class FoundersPCUsersContext : DbContext
    {
        public FoundersPCUsersContext(DbContextOptions<FoundersPCUsersContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }
    }
}