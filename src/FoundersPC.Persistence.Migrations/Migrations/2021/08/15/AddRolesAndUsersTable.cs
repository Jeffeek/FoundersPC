#region Using namespaces

using System;
using System.Data;
using FluentMigrator.SqlServer;
using FoundersPC.Persistence.Migrations.Common;
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
              .WithColumn("IsBlocked").AsBoolean().NotNullable().WithDefaultValue(false)
              .WithColumn("Email").AsString(256).NotNullable()
              .WithColumn("PasswordHash").AsString(60).NotNullable();

        Insert.IntoTable("Users")
              .WithIdentityInsert()
              .Row(new
                   {
                       Id = 1,
                       RoleId = 1,
                       Email = "system.system@founderspc.com",
                       IsBlocked = false,
                       Login = "system",
                       RegistrationDate = DateTime.UtcNow,
                       PasswordHash = "$2b$10$Sa9JgA3l5xK/PiRcifaX..Ag78IwNoKqN1pv5w9P9XbCCI2gcApL6" //123456
                   })
              .Row(new
                   {
                       Id = 2,
                       RoleId = 2,
                       Email = "system.admin@founderspc.com",
                       IsBlocked = false,
                       Login = "admin",
                       RegistrationDate = DateTime.UtcNow,
                       PasswordHash = "$2b$10$D5Rp9tDOiTe4gsAiL5TyoO/4bBUJ/jZBj5kql3leDS.SooWY.S6d." //123456
                   })
              .Row(new
                   {
                       Id = 3,
                       RoleId = 3,
                       Email = "system.manager@founderspc.com",
                       IsBlocked = false,
                       Login = "manager",
                       RegistrationDate = DateTime.UtcNow,
                       PasswordHash = "$2b$10$pGjlIrjkULToE765j00ZLuDe2uMATUruRCRZBg.Q/hkV3lNnjjNuO" //123456
                   })
              .Row(new
                   {
                       Id = 4,
                       RoleId = 4,
                       Email = "system.user@founderspc.com",
                       IsBlocked = false,
                       Login = "user",
                       RegistrationDate = DateTime.UtcNow,
                       PasswordHash = "$2b$10$82A0FoDt7Ky75Cs5HGVGQem0oSZZrCjHYnsEh.reO.6c6koDAXuSC" //123456
                   });
    }

    public override void Down()
    {
        DropDatabaseIfExists();
    }
}