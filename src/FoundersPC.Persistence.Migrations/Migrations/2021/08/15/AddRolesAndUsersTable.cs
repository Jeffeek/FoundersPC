#region Using namespaces

using System;
using System.Data;
using FluentMigrator.SqlServer;
using FoundersPC.Persistence.Migrations.Common;
using FoundersPC.Persistence.Migrations.Extensions;
using FoundersPC.SharedKernel.ApplicationConstants;

#endregion

namespace FoundersPC.Persistence.Migrations.Migrations._2021._08._15;

[FoundersPCMigration(2021_08_15_1, "Add Roles And Users Table")]
public class AddRolesAndUsersTable : MigrationBase
{
    public override void Up()
    {
        Create.Table("Roles")
              .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey("PK_Roles")
              .WithColumn("Name").AsString().NotNullable();

        Insert.IntoTable("Roles")
              .WithIdentityInsert()
              .Row(new
                   {
                       Id = 1,
                       Name = ApplicationRoles.System
                   })
              .Row(new
                   {
                       Id = 2,
                       Name = ApplicationRoles.Administrator
                   })
              .Row(new
                   {
                       Id = 3,
                       Name = ApplicationRoles.Manager
                   })
              .Row(new
                   {
                       Id = 4,
                       Name = ApplicationRoles.DefaultUser
                   });

        Create.Table("Users")
              .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey("PK_Users")
              .WithColumn("Login").AsString(128).NotNullable()
              .WithColumn("RegistrationDate").AsDateTime().NotNullable()
              .WithColumn("RoleId").AsInt32().NotNullable().ForeignKey("FK_Users_Roles_RoleId", "Roles", "Id").OnDeleteOrUpdate(Rule.Cascade)
              .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
              .WithColumn("IsBlocked").AsBoolean().NotNullable().WithDefaultValue(false)
              .WithColumn("SendMessageOnEntrance").AsBoolean().NotNullable().WithDefaultValue(false)
              .WithColumn("SendMessageOnApiRequest").AsBoolean().NotNullable().WithDefaultValue(false)
              .WithColumn("Email").AsString(256).NotNullable()
              .WithColumn("EmailConfirmed").AsBoolean().WithDefaultValue(false).NotNullable()
              .WithColumn("PasswordHash").AsString(60).NotNullable();

        Insert.IntoTable("Users")
              .WithIdentityInsert()
              .Row(new
                   {
                       Id = 1,
                       RoleId = 1,
                       Email = "system.system@founderspc.com",
                       EmailConfirmed = true,
                       IsActive = true,
                       IsBlocked = false,
                       Login = "System",
                       RegistrationDate = DateTime.UtcNow,
                       SendMessageOnApiRequest = true,
                       SendMessageOnEntrance = true,
                       PasswordHash = "$2b$10$wAzjgokpk61UNscVxcA1GuuMQ7ypYIUPwznP6WyNSxTqKfKTurANW"
                   })
              .Row(new
                   {
                       Id = 2,
                       RoleId = 2,
                       Email = "system.admin@founderspc.com",
                       EmailConfirmed = true,
                       IsActive = true,
                       IsBlocked = false,
                       Login = "System",
                       RegistrationDate = DateTime.UtcNow,
                       SendMessageOnApiRequest = true,
                       SendMessageOnEntrance = true,
                       PasswordHash = "$2b$10$9XdL15RJ.uzT3voIZ.UUVOzEOgrelFeH0rIyL9CYFcMffFwpoWVv6"
                   })
              .Row(new
                   {
                       Id = 3,
                       RoleId = 3,
                       Email = "system.manager@founderspc.com",
                       EmailConfirmed = true,
                       IsActive = true,
                       IsBlocked = false,
                       Login = "System",
                       RegistrationDate = DateTime.UtcNow,
                       SendMessageOnApiRequest = true,
                       SendMessageOnEntrance = true,
                       PasswordHash = "$2b$10$Se5VVLreFYE9lrWDPDFvNONapIBc8yiV90vqfD787RvJKISObYudq"
                   })
              .Row(new
                   {
                       Id = 4,
                       RoleId = 4,
                       Email = "system.user@founderspc.com",
                       EmailConfirmed = true,
                       IsActive = true,
                       IsBlocked = false,
                       Login = "System",
                       RegistrationDate = DateTime.UtcNow,
                       SendMessageOnApiRequest = true,
                       SendMessageOnEntrance = true,
                       PasswordHash = "$2b$10$erUjwlGkqPirREocjAPbge2wDkYoHkgs.Vgf0eS6x5nk8U1aA6CAq"
                   });
    }

    public override void Down()
    {
        DropDatabaseIfExists();
    }
}