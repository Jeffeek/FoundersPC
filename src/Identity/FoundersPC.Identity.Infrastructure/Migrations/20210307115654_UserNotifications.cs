#region Using namespaces

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Identity.Infrastructure.Migrations
{
    public partial class UserNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>("SendMessageOnApiRequest",
                                             "Users",
                                             "bit",
                                             nullable : false,
                                             defaultValue : false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("SendMessageOnApiRequest",
                                        "Users");
        }
    }
}