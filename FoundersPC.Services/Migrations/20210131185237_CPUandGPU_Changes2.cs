using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Services.Migrations
{
    public partial class CPUandGPU_Changes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Core",
                table: "ProcessorCores",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ProcessorCores",
                newName: "Core");
        }
    }
}
