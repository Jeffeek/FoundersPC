using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Services.Migrations
{
    public partial class CpuThreads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Threads",
                table: "Processors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Threads",
                table: "Processors");
        }
    }
}
