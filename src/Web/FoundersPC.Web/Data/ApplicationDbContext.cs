#region Using namespaces

using FoundersPC.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<ApplicationRole> Roles { get; set; }
    }
}