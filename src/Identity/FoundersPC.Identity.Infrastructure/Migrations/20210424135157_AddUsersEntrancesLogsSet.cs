#region Using namespaces

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Identity.Infrastructure.Migrations
{
    public partial class AddUsersEntrancesLogsSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_UserEntranceLog_Users_UserId",
                                            "UserEntranceLog");

            migrationBuilder.DropPrimaryKey("PK_UserEntranceLog",
                                            "UserEntranceLog");

            migrationBuilder.RenameTable("UserEntranceLog",
                                         newName : "UsersEntrancesLogs");

            migrationBuilder.RenameIndex("IX_UserEntranceLog_UserId",
                                         table : "UsersEntrancesLogs",
                                         newName : "IX_UsersEntrancesLogs_UserId");

            migrationBuilder.RenameIndex("IX_UserEntranceLog_Id",
                                         table : "UsersEntrancesLogs",
                                         newName : "IX_UsersEntrancesLogs_Id");

            migrationBuilder.AddPrimaryKey("PK_UsersEntrancesLogs",
                                           "UsersEntrancesLogs",
                                           "Id");

            migrationBuilder.AddForeignKey("FK_UsersEntrancesLogs_Users_UserId",
                                           "UsersEntrancesLogs",
                                           "UserId",
                                           "Users",
                                           principalColumn : "Id",
                                           onDelete : ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_UsersEntrancesLogs_Users_UserId",
                                            "UsersEntrancesLogs");

            migrationBuilder.DropPrimaryKey("PK_UsersEntrancesLogs",
                                            "UsersEntrancesLogs");

            migrationBuilder.RenameTable("UsersEntrancesLogs",
                                         newName : "UserEntranceLog");

            migrationBuilder.RenameIndex("IX_UsersEntrancesLogs_UserId",
                                         table : "UserEntranceLog",
                                         newName : "IX_UserEntranceLog_UserId");

            migrationBuilder.RenameIndex("IX_UsersEntrancesLogs_Id",
                                         table : "UserEntranceLog",
                                         newName : "IX_UserEntranceLog_Id");

            migrationBuilder.AddPrimaryKey("PK_UserEntranceLog",
                                           "UserEntranceLog",
                                           "Id");

            migrationBuilder.AddForeignKey("FK_UserEntranceLog_Users_UserId",
                                           "UserEntranceLog",
                                           "UserId",
                                           "Users",
                                           principalColumn : "Id",
                                           onDelete : ReferentialAction.Cascade);
        }
    }
}