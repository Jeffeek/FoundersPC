#region Using namespaces

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Web.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Roles",
                                         table => new
                                                  {
                                                      Id = table.Column<int>("int", nullable : false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                                      RoleTitle = table.Column<string>("nvarchar(max)", nullable : false)
                                                  },
                                         constraints : table => { table.PrimaryKey("PK_Roles", x => x.Id); });

            migrationBuilder.CreateTable("Users",
                                         table => new
                                                  {
                                                      Id = table.Column<int>("int", nullable : false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                                      Login = table.Column<string>("nvarchar(20)", maxLength : 20, nullable : true),
                                                      Email = table.Column<string>("nvarchar(max)", nullable : false),
                                                      PasswordHash = table.Column<string>("nvarchar(max)", nullable : false),
                                                      CreatedAt = table.Column<DateTime>("datetime2", nullable : false),
                                                      IsEmailConfirmed = table.Column<bool>("bit", nullable : false),
                                                      RoleId = table.Column<int>("int", nullable : false),
                                                      IsActive = table.Column<bool>("bit", nullable : false)
                                                  },
                                         constraints : table =>
                                                       {
                                                           table.PrimaryKey("PK_Users", x => x.Id);
                                                           table.ForeignKey("FK_Users_Roles_RoleId",
                                                                            x => x.RoleId,
                                                                            "Roles",
                                                                            "Id",
                                                                            onDelete : ReferentialAction.Cascade);
                                                       });

            migrationBuilder.CreateIndex("IX_Roles_Id",
                                         "Roles",
                                         "Id");

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