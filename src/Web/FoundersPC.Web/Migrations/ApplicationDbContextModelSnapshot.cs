﻿#region Using namespaces

using System;
using FoundersPC.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#endregion

namespace FoundersPC.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    internal class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            #pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FoundersPC.Web.Data.Entities.ApplicationRole",
                                b =>
                                {
                                    b.Property<int>("Id")
                                     .ValueGeneratedOnAdd()
                                     .HasColumnType("int")
                                     .HasColumnName("Id")
                                     .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b.Property<string>("RoleTitle")
                                     .IsRequired()
                                     .HasColumnType("nvarchar(max)");

                                    b.HasKey("Id");

                                    b.HasIndex("Id");

                                    b.ToTable("Roles");
                                });

            modelBuilder.Entity("FoundersPC.Web.Data.Entities.ApplicationUser",
                                b =>
                                {
                                    b.Property<int>("Id")
                                     .ValueGeneratedOnAdd()
                                     .HasColumnType("int")
                                     .HasColumnName("Id")
                                     .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b.Property<DateTime>("CreatedAt")
                                     .HasColumnType("datetime2");

                                    b.Property<string>("Email")
                                     .IsRequired()
                                     .HasColumnType("nvarchar(max)");

                                    b.Property<bool>("IsActive")
                                     .HasColumnType("bit");

                                    b.Property<bool>("IsEmailConfirmed")
                                     .HasColumnType("bit");

                                    b.Property<string>("Login")
                                     .HasMaxLength(20)
                                     .HasColumnType("nvarchar(20)");

                                    b.Property<string>("PasswordHash")
                                     .IsRequired()
                                     .HasColumnType("nvarchar(max)");

                                    b.Property<int>("RoleId")
                                     .HasColumnType("int");

                                    b.HasKey("Id");

                                    b.HasIndex("Id");

                                    b.HasIndex("RoleId");

                                    b.ToTable("Users");
                                });

            modelBuilder.Entity("FoundersPC.Web.Data.Entities.ApplicationUser",
                                b =>
                                {
                                    b.HasOne("FoundersPC.Web.Data.Entities.ApplicationRole", "Role")
                                     .WithMany("Users")
                                     .HasForeignKey("RoleId")
                                     .OnDelete(DeleteBehavior.Cascade)
                                     .IsRequired();

                                    b.Navigation("Role");
                                });

            modelBuilder.Entity("FoundersPC.Web.Data.Entities.ApplicationRole", b => { b.Navigation("Users"); });
            #pragma warning restore 612, 618
        }
    }
}