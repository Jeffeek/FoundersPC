using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Services.Migrations
{
    public partial class CPUandGPU_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processors_ProcessorLineups_ProcessorLineupId",
                table: "Processors");

            migrationBuilder.DropTable(
                name: "ProcessorLineups");

            migrationBuilder.DropColumn(
                name: "DirectX",
                table: "VideoCards");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "VideoCards");

            migrationBuilder.DropColumn(
                name: "GraphicsProcessor",
                table: "VideoCards");

            migrationBuilder.DropColumn(
                name: "Interface",
                table: "VideoCards");

            migrationBuilder.DropColumn(
                name: "SLI_Crossfire",
                table: "VideoCards");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "VideoCards",
                newName: "VideoMemoryVolume");

            migrationBuilder.RenameColumn(
                name: "ProcessorLineupId",
                table: "Processors",
                newName: "ProcessorCoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Processors_ProcessorLineupId",
                table: "Processors",
                newName: "IX_Processors_ProcessorCoreId");

            migrationBuilder.AddColumn<int>(
                name: "GraphicsProcessorId",
                table: "VideoCards",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProcessorCores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Core = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MicroArchitecture = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TechProcess = table.Column<int>(type: "int", nullable: false),
                    L1Cache = table.Column<int>(type: "int", nullable: false),
                    L2Cache = table.Column<int>(type: "int", nullable: false),
                    MaxL3Cache = table.Column<int>(type: "int", nullable: false),
                    Socket = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessorCores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoCardCores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TechProcess = table.Column<int>(type: "int", nullable: false),
                    MaxResolution = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MonitorsSupport = table.Column<int>(type: "int", nullable: false),
                    Interface = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Frequency = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    DirectX_Version = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    SLI_Crossfire = table.Column<bool>(type: "bit", nullable: false),
                    ArchitectureTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoCardCores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoCards_GraphicsProcessorId",
                table: "VideoCards",
                column: "GraphicsProcessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processors_ProcessorCores_ProcessorCoreId",
                table: "Processors",
                column: "ProcessorCoreId",
                principalTable: "ProcessorCores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoCards_VideoCardCores_GraphicsProcessorId",
                table: "VideoCards",
                column: "GraphicsProcessorId",
                principalTable: "VideoCardCores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processors_ProcessorCores_ProcessorCoreId",
                table: "Processors");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoCards_VideoCardCores_GraphicsProcessorId",
                table: "VideoCards");

            migrationBuilder.DropTable(
                name: "ProcessorCores");

            migrationBuilder.DropTable(
                name: "VideoCardCores");

            migrationBuilder.DropIndex(
                name: "IX_VideoCards_GraphicsProcessorId",
                table: "VideoCards");

            migrationBuilder.DropColumn(
                name: "GraphicsProcessorId",
                table: "VideoCards");

            migrationBuilder.RenameColumn(
                name: "VideoMemoryVolume",
                table: "VideoCards",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "ProcessorCoreId",
                table: "Processors",
                newName: "ProcessorLineupId");

            migrationBuilder.RenameIndex(
                name: "IX_Processors_ProcessorCoreId",
                table: "Processors",
                newName: "IX_Processors_ProcessorLineupId");

            migrationBuilder.AddColumn<int>(
                name: "DirectX",
                table: "VideoCards",
                type: "int",
                maxLength: 3,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "VideoCards",
                type: "int",
                maxLength: 5,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GraphicsProcessor",
                table: "VideoCards",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Interface",
                table: "VideoCards",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SLI_Crossfire",
                table: "VideoCards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ProcessorLineups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyCodename = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MicroArchitecture = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Socket = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
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
    }
}
