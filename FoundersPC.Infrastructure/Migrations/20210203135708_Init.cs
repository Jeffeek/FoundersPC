using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessorCores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MicroArchitecture = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    L2CachePerCore = table.Column<int>(type: "int", nullable: false),
                    L3CachePerCore = table.Column<int>(type: "int", nullable: false),
                    Socket = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessorCores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FoundationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    MaxMotherboardSize = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Material = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WindowMaterial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TransparentWindow = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HardDrives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeadSpeed = table.Column<int>(type: "int", nullable: false),
                    BufferSize = table.Column<int>(type: "int", nullable: false),
                    Noise = table.Column<int>(type: "int", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Factor = table.Column<double>(type: "float", nullable: false),
                    Interface = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardDrives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardDrives_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motherboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Socket = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Factor = table.Column<double>(type: "float", nullable: false),
                    RAMSupport = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    RAMSlots = table.Column<int>(type: "int", nullable: false),
                    RAMMode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    SLI_Crossfire = table.Column<bool>(type: "bit", nullable: false),
                    AudioSupport = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WiFiSupport = table.Column<bool>(type: "bit", nullable: false),
                    PS2Support = table.Column<bool>(type: "bit", nullable: false),
                    M2SlotsCount = table.Column<int>(type: "int", nullable: false),
                    PCIExpressVersion = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motherboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motherboards_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Power = table.Column<int>(type: "int", nullable: false),
                    Efficiency = table.Column<int>(type: "int", nullable: false),
                    MotherboardPowering = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsModular = table.Column<bool>(type: "bit", nullable: false),
                    CPU4PIN = table.Column<bool>(type: "bit", nullable: false),
                    CPU8PIN = table.Column<bool>(type: "bit", nullable: false),
                    FanDiameter = table.Column<int>(type: "int", nullable: false),
                    Certificate80PLUS = table.Column<bool>(type: "bit", nullable: false),
                    PFC = table.Column<bool>(type: "bit", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerSupplies_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Processors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TDP = table.Column<int>(type: "int", nullable: false),
                    ProcessorCoreId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaxRamSpeed = table.Column<int>(type: "int", nullable: false),
                    Cores = table.Column<int>(type: "int", nullable: false),
                    Threads = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    TurboBoostFrequency = table.Column<int>(type: "int", nullable: false),
                    TechProcess = table.Column<int>(type: "int", nullable: false),
                    L1Cache = table.Column<int>(type: "int", nullable: false),
                    L2Cache = table.Column<int>(type: "int", nullable: false),
                    L3Cache = table.Column<int>(type: "int", nullable: false),
                    IntegratedGraphics = table.Column<bool>(type: "bit", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processors_ProcessorCores_ProcessorCoreId",
                        column: x => x.ProcessorCoreId,
                        principalTable: "ProcessorCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Processors_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RandomAccessMemory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemoryType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    CASLatency = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Timings = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Voltage = table.Column<double>(type: "float", nullable: false),
                    XMP = table.Column<bool>(type: "bit", nullable: false),
                    ECC = table.Column<bool>(type: "bit", nullable: false),
                    PCIndex = table.Column<int>(type: "int", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandomAccessMemory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RandomAccessMemory_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolidStateDrives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MicroScheme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SequentialRead = table.Column<int>(type: "int", nullable: false),
                    SequentialRecording = table.Column<int>(type: "int", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Factor = table.Column<double>(type: "float", nullable: false),
                    Interface = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolidStateDrives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolidStateDrives_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GraphicsProcessorId = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    AdditionalPower = table.Column<int>(type: "int", nullable: false),
                    VideoMemoryVolume = table.Column<int>(type: "int", nullable: false),
                    VideoMemoryType = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    VideoMemoryFrequency = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    VideoMemoryBusWidth = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    VGA = table.Column<int>(type: "int", nullable: false),
                    DVI = table.Column<int>(type: "int", nullable: false),
                    HDMI = table.Column<int>(type: "int", nullable: false),
                    DisplayPort = table.Column<int>(type: "int", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    MarketLaunch = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoCards_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoCards_VideoCardCores_GraphicsProcessorId",
                        column: x => x.GraphicsProcessorId,
                        principalTable: "VideoCardCores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_Id",
                table: "Cases",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ProducerId",
                table: "Cases",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_HardDrives_Id",
                table: "HardDrives",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HardDrives_ProducerId",
                table: "HardDrives",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_Id",
                table: "Motherboards",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboards_ProducerId",
                table: "Motherboards",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplies_Id",
                table: "PowerSupplies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplies_ProducerId",
                table: "PowerSupplies",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Processors_Id",
                table: "Processors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Processors_ProcessorCoreId",
                table: "Processors",
                column: "ProcessorCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Processors_ProducerId",
                table: "Processors",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Producers_Id",
                table: "Producers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RandomAccessMemory_Id",
                table: "RandomAccessMemory",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RandomAccessMemory_ProducerId",
                table: "RandomAccessMemory",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_SolidStateDrives_Id",
                table: "SolidStateDrives",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SolidStateDrives_ProducerId",
                table: "SolidStateDrives",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoCards_GraphicsProcessorId",
                table: "VideoCards",
                column: "GraphicsProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoCards_Id",
                table: "VideoCards",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VideoCards_ProducerId",
                table: "VideoCards",
                column: "ProducerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "HardDrives");

            migrationBuilder.DropTable(
                name: "Motherboards");

            migrationBuilder.DropTable(
                name: "PowerSupplies");

            migrationBuilder.DropTable(
                name: "Processors");

            migrationBuilder.DropTable(
                name: "RandomAccessMemory");

            migrationBuilder.DropTable(
                name: "SolidStateDrives");

            migrationBuilder.DropTable(
                name: "VideoCards");

            migrationBuilder.DropTable(
                name: "ProcessorCores");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropTable(
                name: "VideoCardCores");
        }
    }
}
