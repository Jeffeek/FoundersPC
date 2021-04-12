#region Using namespaces

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Identity.Infrastructure.Migrations
{
    public partial class AddEmalIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_Users_Id",
                                       "Users");

            migrationBuilder.CreateIndex("ix_users_email",
                                         "Users",
                                         "Email",
                                         unique : true);

            migrationBuilder.CreateIndex("ix_users_id",
                                         "Users",
                                         "Id",
                                         unique : true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("ix_users_email",
                                       "Users");

            migrationBuilder.DropIndex("ix_users_id",
                                       "Users");

            migrationBuilder.CreateIndex("IX_Users_Id",
                                         "Users",
                                         "Id");
        }
    }
}