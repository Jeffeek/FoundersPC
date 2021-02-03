using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Infrastructure.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Processors",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "VideoCards",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "Processors",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "VideoCards");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "Processors");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Processors",
                newName: "Name");
        }
    }
}
