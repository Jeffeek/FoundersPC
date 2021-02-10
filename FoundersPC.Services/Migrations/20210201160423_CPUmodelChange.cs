using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Services.Migrations
{
    public partial class CPUmodelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "L1Cache",
                table: "ProcessorCores");

            migrationBuilder.RenameColumn(
                name: "MaxL3Cache",
                table: "ProcessorCores",
                newName: "L3CachePerCore");

            migrationBuilder.RenameColumn(
                name: "L2Cache",
                table: "ProcessorCores",
                newName: "L2CachePerCore");

            migrationBuilder.AddColumn<int>(
                name: "L1Cache",
                table: "Processors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "L2Cache",
                table: "Processors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "L1Cache",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "L2Cache",
                table: "Processors");

            migrationBuilder.RenameColumn(
                name: "L3CachePerCore",
                table: "ProcessorCores",
                newName: "MaxL3Cache");

            migrationBuilder.RenameColumn(
                name: "L2CachePerCore",
                table: "ProcessorCores",
                newName: "L2Cache");

            migrationBuilder.AddColumn<int>(
                name: "L1Cache",
                table: "ProcessorCores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
