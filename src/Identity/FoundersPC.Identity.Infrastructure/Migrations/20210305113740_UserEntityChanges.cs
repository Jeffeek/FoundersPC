using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Identity.Infrastructure.Migrations
{
    public partial class UserEntityChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                                             name: "SendMessageOnEntrance",
                                             table: "Users",
                                             type: "bit",
                                             nullable: false,
                                             defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendMessageOnEntrance",
                table: "Users");
        }
    }
}
