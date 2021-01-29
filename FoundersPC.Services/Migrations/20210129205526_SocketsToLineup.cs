using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Services.Migrations
{
    public partial class SocketsToLineup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Socket",
                table: "Processors");

            migrationBuilder.AddColumn<string>(
                name: "Socket",
                table: "ProcessorLineups",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Socket",
                table: "ProcessorLineups");

            migrationBuilder.AddColumn<string>(
                name: "Socket",
                table: "Processors",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
