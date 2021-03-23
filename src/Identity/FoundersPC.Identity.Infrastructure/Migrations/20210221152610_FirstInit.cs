#region Using namespaces

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Identity.Infrastructure.Migrations
{
    public partial class FirstInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Roles",
                                         table => new
                                                  {
                                                      Id = table.Column<int>("int", nullable : false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                                      RoleTitle = table.Column<string>("nvarchar(max)",
                                                          nullable : false)
                                                  },
                                         constraints : table => { table.PrimaryKey("PK_Roles", x => x.Id); });

            migrationBuilder.CreateTable("Users",
                                         table => new
                                                  {
                                                      Id = table.Column<int>("int", nullable : false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                                      Login =
                                                          table.Column<string>("nvarchar(30)",
                                                                               maxLength : 30,
                                                                               nullable : true),
                                                      RegistrationDate =
                                                          table.Column<DateTime>("datetime2", nullable : false),
                                                      RoleId = table.Column<int>("int", nullable : false),
                                                      IsActive =
                                                          table.Column<bool>("bit",
                                                                             nullable : false,
                                                                             defaultValue : true),
                                                      IsBlocked =
                                                          table.Column<bool>("bit",
                                                                             nullable : false,
                                                                             defaultValue : false),
                                                      Email =
                                                          table.Column<string>("nvarchar(128)",
                                                                               maxLength : 128,
                                                                               nullable : false),
                                                      HashedPassword =
                                                          table.Column<string>("nvarchar(128)",
                                                                               maxLength : 128,
                                                                               nullable : false)
                                                  },
                                         constraints : table =>
                                                       {
                                                           table.PrimaryKey("PK_Users", x => x.Id);

                                                           table.ForeignKey("FK_Users_Roles_RoleId",
                                                                            x => x.RoleId,
                                                                            "Roles",
                                                                            "Id",
                                                                            onDelete : ReferentialAction.Restrict);
                                                       });

            migrationBuilder.CreateIndex("IX_Users_Id",
                                         "Users",
                                         "Id");

            migrationBuilder.CreateIndex("IX_Users_RoleId",
                                         "Users",
                                         "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Users");

            migrationBuilder.DropTable("Roles");
        }
    }
}