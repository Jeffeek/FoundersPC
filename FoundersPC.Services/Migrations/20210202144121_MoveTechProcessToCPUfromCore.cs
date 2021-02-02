using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Services.Migrations
{
    public partial class MoveTechProcessToCPUfromCore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechProcess",
                table: "ProcessorCores");

            migrationBuilder.AddColumn<int>(
                name: "TechProcess",
                table: "Processors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechProcess",
                table: "Processors");

            migrationBuilder.AddColumn<int>(
                name: "TechProcess",
                table: "ProcessorCores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
