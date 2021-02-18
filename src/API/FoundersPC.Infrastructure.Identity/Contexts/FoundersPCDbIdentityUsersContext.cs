#region Using namespaces

using FoundersPC.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Identity.Contexts
{
    public class FoundersPCDbIdentityUsersContext : DbContext
    {
        public FoundersPCDbIdentityUsersContext(DbContextOptions<FoundersPCDbIdentityUsersContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }
    }
}