#region Using namespaces

using System;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Contexts
{
    public class FoundersPCUsersContext : DbContext
    {
        public FoundersPCUsersContext(DbContextOptions<FoundersPCUsersContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<AccessTokenEntity> Tokens { get; set; }

        public DbSet<AccessTokenLog> TokenAccessLogs { get; set; }

        public DbSet<UserEntranceLog> UsersEntrancesLogs { get; set; }

        #region Overrides of DbContext

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoleEntity>(x => x.HasData(new RoleEntity
                                                           {
                                                               RoleTitle = "Administrator",
                                                               Id = 1
                                                           },
                                                           new RoleEntity
                                                           {
                                                               RoleTitle = "Manager",
                                                               Id = 2
                                                           },
                                                           new RoleEntity
                                                           {
                                                               RoleTitle = "DefaultUser",
                                                               Id = 3
                                                           }));

            // default passwords: qwerty
            modelBuilder.Entity<UserEntity>(x => x.HasData(new UserEntity
                                                           {
                                                               Email = "exampleAdmin@mail.com",
                                                               HashedPassword =
                                                                   "0DD3E512642C97CA3F747F9A76E374FBDA73F9292823C0313BE9D78ADD7CDD8F72235AF0C553DD26797E78E1854EDEE0AE002F8ABA074B066DFCE1AF114E32F8",
                                                               IsActive = true,
                                                               IsBlocked = false,
                                                               Login = "Administrator",
                                                               RegistrationDate = new DateTime(2020,
                                                                                               01,
                                                                                               01,
                                                                                               1,
                                                                                               1,
                                                                                               1,
                                                                                               DateTimeKind.Utc),
                                                               RoleId = 1,
                                                               SendMessageOnEntrance = false,
                                                               SendMessageOnApiRequest = false,
                                                               Id = 1
                                                           },
                                                           new UserEntity
                                                           {
                                                               Email = "exampleManager@mail.com",
                                                               HashedPassword =
                                                                   "0DD3E512642C97CA3F747F9A76E374FBDA73F9292823C0313BE9D78ADD7CDD8F72235AF0C553DD26797E78E1854EDEE0AE002F8ABA074B066DFCE1AF114E32F8",
                                                               IsActive = true,
                                                               IsBlocked = false,
                                                               Login = "Manager",
                                                               RegistrationDate = new DateTime(2020,
                                                                                               01,
                                                                                               01,
                                                                                               1,
                                                                                               1,
                                                                                               1,
                                                                                               DateTimeKind.Utc),
                                                               RoleId = 2,
                                                               SendMessageOnEntrance = false,
                                                               SendMessageOnApiRequest = false,
                                                               Id = 2
                                                           },
                                                           new UserEntity
                                                           {
                                                               Email = "exampleDefaultUser@mail.com",
                                                               HashedPassword =
                                                                   "0DD3E512642C97CA3F747F9A76E374FBDA73F9292823C0313BE9D78ADD7CDD8F72235AF0C553DD26797E78E1854EDEE0AE002F8ABA074B066DFCE1AF114E32F8",
                                                               IsActive = true,
                                                               IsBlocked = false,
                                                               Login = "Manager",
                                                               RegistrationDate = new DateTime(2020,
                                                                                               01,
                                                                                               01,
                                                                                               1,
                                                                                               1,
                                                                                               1,
                                                                                               DateTimeKind.Utc),
                                                               RoleId = 3,
                                                               SendMessageOnEntrance = false,
                                                               SendMessageOnApiRequest = false,
                                                               Id = 3
                                                           }));
        }

        #endregion
    }
}