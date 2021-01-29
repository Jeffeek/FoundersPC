using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Services.Migrations
{
    public partial class InitCPULineUps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processors_ProcessorLineup_ProcessorLineupId",
                table: "Processors");

            migrationBuilder.DropTable(
                name: "ProcessorLineup");

            migrationBuilder.AlterColumn<string>(
                name: "MicroScheme",
                table: "SolidStateDrives",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Interface",
                table: "SolidStateDrives",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Timings",
                table: "RandomAccessMemory",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MemoryType",
                table: "RandomAccessMemory",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CASLatency",
                table: "RandomAccessMemory",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Interface",
                table: "HardDrives",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProcessorLineups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FamilyCodename = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MicroArchitecture = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TechProcess = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessorLineups", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Processors_ProcessorLineups_ProcessorLineupId",
                table: "Processors",
                column: "ProcessorLineupId",
                principalTable: "ProcessorLineups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processors_ProcessorLineups_ProcessorLineupId",
                table: "Processors");

            migrationBuilder.DropTable(
                name: "ProcessorLineups");

            migrationBuilder.AlterColumn<string>(
                name: "MicroScheme",
                table: "SolidStateDrives",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Interface",
                table: "SolidStateDrives",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Timings",
                table: "RandomAccessMemory",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "MemoryType",
                table: "RandomAccessMemory",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "CASLatency",
                table: "RandomAccessMemory",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Interface",
                table: "HardDrives",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "ProcessorLineup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyCodename = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MicroArchitecture = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TechProcess = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessorLineup", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Processors_ProcessorLineup_ProcessorLineupId",
                table: "Processors",
                column: "ProcessorLineupId",
                principalTable: "ProcessorLineup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
