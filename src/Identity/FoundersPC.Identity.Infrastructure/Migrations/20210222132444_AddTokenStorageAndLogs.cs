#region Using namespaces

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Identity.Infrastructure.Migrations
{
    public partial class AddTokenStorageAndLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Users_Roles_RoleId",
                                            "Users");

            migrationBuilder.CreateTable("ApiTokens",
                                         table => new
                                                  {
                                                      Id = table.Column<int>("int", nullable : false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                                      HashedToken = table.Column<string>("nvarchar(88)", maxLength : 88, nullable : false)
                                                  },
                                         constraints : table => { table.PrimaryKey("PK_ApiTokens", x => x.Id); });

            migrationBuilder.CreateTable("UserEntranceLog",
                                         table => new
                                                  {
                                                      Id = table.Column<int>("int", nullable : false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                                      UserId = table.Column<int>("int", nullable : false),
                                                      Entrance = table.Column<DateTime>("datetime2", nullable : false)
                                                  },
                                         constraints : table =>
                                                       {
                                                           table.PrimaryKey("PK_UserEntranceLog", x => x.Id);

                                                           table.ForeignKey("FK_UserEntranceLog_Users_UserId",
                                                                            x => x.UserId,
                                                                            "Users",
                                                                            "Id",
                                                                            onDelete : ReferentialAction.Restrict);
                                                       });

            migrationBuilder.CreateTable("UsersTokens",
                                         table => new
                                                  {
                                                      Id = table.Column<int>("int", nullable : false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                                      ApiAccessTokenId = table.Column<int>("int", nullable : false),
                                                      UserId = table.Column<int>("int", nullable : false),
                                                      StartEvaluationDate = table.Column<DateTime>("datetime2", nullable : false),
                                                      ExpirationDate = table.Column<DateTime>("datetime2", nullable : false),
                                                      IsBlocked = table.Column<bool>("bit", nullable : false)
                                                  },
                                         constraints : table =>
                                                       {
                                                           table.PrimaryKey("PK_UsersTokens", x => x.Id);

                                                           table.ForeignKey("FK_UsersTokens_ApiTokens_ApiAccessTokenId",
                                                                            x => x.ApiAccessTokenId,
                                                                            "ApiTokens",
                                                                            "Id",
                                                                            onDelete : ReferentialAction.Restrict);

                                                           table.ForeignKey("FK_UsersTokens_Users_UserId",
                                                                            x => x.UserId,
                                                                            "Users",
                                                                            "Id",
                                                                            onDelete : ReferentialAction.Restrict);
                                                       });

            migrationBuilder.CreateTable("TokenAccessLogs",
                                         table => new
                                                  {
                                                      Id = table.Column<int>("int", nullable : false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                                      ApiAccessUsersTokenId = table.Column<int>("int", nullable : false),
                                                      RequestDateTime = table.Column<DateTime>("datetime2", nullable : false)
                                                  },
                                         constraints : table =>
                                                       {
                                                           table.PrimaryKey("PK_TokenAccessLogs", x => x.Id);

                                                           table.ForeignKey("FK_TokenAccessLogs_UsersTokens_ApiAccessUsersTokenId",
                                                                            x => x.ApiAccessUsersTokenId,
                                                                            "UsersTokens",
                                                                            "Id",
                                                                            onDelete : ReferentialAction.Restrict);
                                                       });

            migrationBuilder.CreateIndex("IX_ApiTokens_Id",
                                         "ApiTokens",
                                         "Id");

            migrationBuilder.CreateIndex("IX_TokenAccessLogs_ApiAccessUsersTokenId",
                                         "TokenAccessLogs",
                                         "ApiAccessUsersTokenId");

            migrationBuilder.CreateIndex("IX_TokenAccessLogs_Id",
                                         "TokenAccessLogs",
                                         "Id");

            migrationBuilder.CreateIndex("IX_UserEntranceLog_Id",
                                         "UserEntranceLog",
                                         "Id");

            migrationBuilder.CreateIndex("IX_UserEntranceLog_UserId",
                                         "UserEntranceLog",
                                         "UserId");

            migrationBuilder.CreateIndex("IX_UsersTokens_ApiAccessTokenId",
                                         "UsersTokens",
                                         "ApiAccessTokenId");

            migrationBuilder.CreateIndex("IX_UsersTokens_Id",
                                         "UsersTokens",
                                         "Id");

            migrationBuilder.CreateIndex("IX_UsersTokens_UserId",
                                         "UsersTokens",
                                         "UserId");

            migrationBuilder.AddForeignKey("FK_Users_Roles_RoleId",
                                           "Users",
                                           "RoleId",
                                           "Roles",
                                           principalColumn : "Id",
                                           onDelete : ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Users_Roles_RoleId",
                                            "Users");

            migrationBuilder.DropTable("TokenAccessLogs");

            migrationBuilder.DropTable("UserEntranceLog");

            migrationBuilder.DropTable("UsersTokens");

            migrationBuilder.DropTable("ApiTokens");

            migrationBuilder.AddForeignKey("FK_Users_Roles_RoleId",
                                           "Users",
                                           "RoleId",
                                           "Roles",
                                           principalColumn : "Id",
                                           onDelete : ReferentialAction.Restrict);
        }
    }
}