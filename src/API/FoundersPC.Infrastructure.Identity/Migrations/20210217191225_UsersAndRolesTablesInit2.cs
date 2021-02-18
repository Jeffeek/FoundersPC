#region Using namespaces

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Infrastructure.Identity.Migrations
{
    public partial class UsersAndRolesTablesInit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>("Login",
                                                 "Users",
                                                 "nvarchar(20)",
                                                 maxLength : 20,
                                                 nullable : true,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(max)",
                                                 oldNullable : true);

            migrationBuilder.AddColumn<bool>("IsActive",
                                             "Users",
                                             "bit",
                                             nullable : false,
                                             defaultValue : false);

            migrationBuilder.AlterColumn<string>("RoleTitle",
                                                 "Roles",
                                                 "nvarchar(max)",
                                                 nullable : false,
                                                 defaultValue : "",
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(max)",
                                                 oldNullable : true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("IsActive",
                                        "Users");

            migrationBuilder.AlterColumn<string>("Login",
                                                 "Users",
                                                 "nvarchar(max)",
                                                 nullable : true,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(20)",
                                                 oldMaxLength : 20,
                                                 oldNullable : true);

            migrationBuilder.AlterColumn<string>("RoleTitle",
                                                 "Roles",
                                                 "nvarchar(max)",
                                                 nullable : true,
                                                 oldClrType : typeof(string),
                                                 oldType : "nvarchar(max)");
        }
    }
}