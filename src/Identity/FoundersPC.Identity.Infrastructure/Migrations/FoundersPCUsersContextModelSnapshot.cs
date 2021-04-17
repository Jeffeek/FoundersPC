#region Using namespaces

using System;
using FoundersPC.Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#endregion

namespace FoundersPC.Identity.Infrastructure.Migrations
{
    [DbContext(typeof(FoundersPCUsersContext))]
    internal class FoundersPCUsersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            #pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FoundersPC.Identity.Domain.Entities.Logs.AccessTokenLog",
                                b =>
                                {
                                    b.Property<int>("Id")
                                     .ValueGeneratedOnAdd()
                                     .HasColumnType("int")
                                     .HasColumnName("Id")
                                     .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                                    SqlServerValueGenerationStrategy.IdentityColumn);

                                    b.Property<int>("ApiAccessUsersTokenId")
                                     .HasColumnType("int");

                                    b.Property<DateTime>("RequestDateTime")
                                     .HasColumnType("datetime2");

                                    b.HasKey("Id");

                                    b.HasIndex("ApiAccessUsersTokenId");

                                    b.HasIndex("Id");

                                    b.ToTable("TokenAccessLogs");
                                });

            modelBuilder.Entity("FoundersPC.Identity.Domain.Entities.Logs.UserEntranceLog",
                                b =>
                                {
                                    b.Property<int>("Id")
                                     .ValueGeneratedOnAdd()
                                     .HasColumnType("int")
                                     .HasColumnName("Id")
                                     .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                                    SqlServerValueGenerationStrategy.IdentityColumn);

                                    b.Property<DateTime>("Entrance")
                                     .HasColumnType("datetime2");

                                    b.Property<int>("UserId")
                                     .HasColumnType("int");

                                    b.HasKey("Id");

                                    b.HasIndex("Id");

                                    b.HasIndex("UserId");

                                    b.ToTable("UserEntranceLog");
                                });

            modelBuilder.Entity("FoundersPC.Identity.Domain.Entities.Tokens.AccessTokenEntity",
                                b =>
                                {
                                    b.Property<int>("Id")
                                     .ValueGeneratedOnAdd()
                                     .HasColumnType("int")
                                     .HasColumnName("Id")
                                     .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                                    SqlServerValueGenerationStrategy.IdentityColumn);

                                    b.Property<DateTime>("ExpirationDate")
                                     .HasColumnType("datetime2");

                                    b.Property<string>("HashedToken")
                                     .IsRequired()
                                     .HasMaxLength(88)
                                     .HasColumnType("nvarchar(88)");

                                    b.Property<bool>("IsBlocked")
                                     .HasColumnType("bit");

                                    b.Property<DateTime>("StartEvaluationDate")
                                     .HasColumnType("datetime2");

                                    b.Property<int>("UserId")
                                     .HasColumnType("int");

                                    b.HasKey("Id");

                                    b.HasIndex("Id");

                                    b.HasIndex("UserId");

                                    b.ToTable("UsersTokens");
                                });

            modelBuilder.Entity("FoundersPC.Identity.Domain.Entities.Users.RoleEntity",
                                b =>
                                {
                                    b.Property<int>("Id")
                                     .ValueGeneratedOnAdd()
                                     .HasColumnType("int")
                                     .HasColumnName("Id")
                                     .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                                    SqlServerValueGenerationStrategy.IdentityColumn);

                                    b.Property<string>("RoleTitle")
                                     .IsRequired()
                                     .HasColumnType("nvarchar(max)");

                                    b.HasKey("Id");

                                    b.ToTable("Roles");
                                });

            modelBuilder.Entity("FoundersPC.Identity.Domain.Entities.Users.UserEntity",
                                b =>
                                {
                                    b.Property<int>("Id")
                                     .ValueGeneratedOnAdd()
                                     .HasColumnType("int")
                                     .HasColumnName("Id")
                                     .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                                    SqlServerValueGenerationStrategy.IdentityColumn);

                                    b.Property<string>("Email")
                                     .IsRequired()
                                     .HasMaxLength(128)
                                     .HasColumnType("nvarchar(128)");

                                    b.Property<string>("HashedPassword")
                                     .IsRequired()
                                     .HasMaxLength(128)
                                     .HasColumnType("nvarchar(128)");

                                    b.Property<bool>("IsActive")
                                     .HasColumnType("bit");

                                    b.Property<bool>("IsBlocked")
                                     .HasColumnType("bit");

                                    b.Property<string>("Login")
                                     .HasMaxLength(30)
                                     .HasColumnType("nvarchar(30)");

                                    b.Property<DateTime>("RegistrationDate")
                                     .HasColumnType("datetime2");

                                    b.Property<int>("RoleId")
                                     .HasColumnType("int");

                                    b.Property<bool>("SendMessageOnApiRequest")
                                     .HasColumnType("bit");

                                    b.Property<bool>("SendMessageOnEntrance")
                                     .HasColumnType("bit");

                                    b.HasKey("Id");

                                    b.HasIndex("RoleId");

                                    b.HasIndex(new[]
                                               {
                                                   "Email"
                                               },
                                               "ix_users_email")
                                     .IsUnique();

                                    b.HasIndex(new[]
                                               {
                                                   "Id"
                                               },
                                               "ix_users_id")
                                     .IsUnique();

                                    b.ToTable("Users");
                                });

            modelBuilder.Entity("FoundersPC.Identity.Domain.Entities.Logs.AccessTokenLog",
                                b =>
                                {
                                    b.HasOne("FoundersPC.Identity.Domain.Entities.Tokens.AccessTokenEntity",
                                             "AccessTokenEntity")
                                     .WithMany()
                                     .HasForeignKey("ApiAccessUsersTokenId")
                                     .OnDelete(DeleteBehavior.Cascade)
                                     .IsRequired();

                                    b.Navigation("AccessTokenEntity");
                                });

            modelBuilder.Entity("FoundersPC.Identity.Domain.Entities.Logs.UserEntranceLog",
                                b =>
                                {
                                    b.HasOne("FoundersPC.Identity.Domain.Entities.Users.UserEntity", "User")
                                     .WithMany("Entrances")
                                     .HasForeignKey("UserId")
                                     .OnDelete(DeleteBehavior.Cascade)
                                     .IsRequired();

                                    b.Navigation("User");
                                });

            modelBuilder.Entity("FoundersPC.Identity.Domain.Entities.Tokens.AccessTokenEntity",
                                b =>
                                {
                                    b.HasOne("FoundersPC.Identity.Domain.Entities.Users.UserEntity", "User")
                                     .WithMany("Tokens")
                                     .HasForeignKey("UserId")
                                     .OnDelete(DeleteBehavior.Cascade)
                                     .IsRequired();

                                    b.Navigation("User");
                                });

            modelBuilder.Entity("FoundersPC.Identity.Domain.Entities.Users.UserEntity",
                                b =>
                                {
                                    b.HasOne("FoundersPC.Identity.Domain.Entities.Users.RoleEntity", "Role")
                                     .WithMany("Users")
                                     .HasForeignKey("RoleId")
                                     .OnDelete(DeleteBehavior.Cascade)
                                     .IsRequired();

                                    b.Navigation("Role");
                                });

            modelBuilder.Entity("FoundersPC.Identity.Domain.Entities.Users.RoleEntity",
                                b => { b.Navigation("Users"); });

            modelBuilder.Entity("FoundersPC.Identity.Domain.Entities.Users.UserEntity",
                                b =>
                                {
                                    b.Navigation("Entrances");

                                    b.Navigation("Tokens");
                                });
            #pragma warning restore 612, 618
        }
    }
}