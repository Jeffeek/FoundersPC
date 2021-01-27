using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundersPC.Services.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChipProducers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChipProducers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FoundationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrystalSerials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChipProducerId = table.Column<int>(type: "int", nullable: false),
                    MicroArchitecture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalSerials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalSerials_ChipProducers_ChipProducerId",
                        column: x => x.ChipProducerId,
                        principalTable: "ChipProducers",
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
                    Volume = table.Column<int>(type: "int", nullable: false),
                    Factor = table.Column<double>(type: "float", nullable: false),
                    Interface = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
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
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    Socket = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Factor = table.Column<double>(type: "float", nullable: false),
                    RAMSupport = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    RAMSlots = table.Column<int>(type: "int", nullable: false),
                    RAMMode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    SLI_Crossfire = table.Column<bool>(type: "bit", nullable: false),
                    AudioSupport = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WiFiSupport = table.Column<bool>(type: "bit", nullable: false),
                    PS2Support = table.Column<bool>(type: "bit", nullable: false),
                    M2SlotsCount = table.Column<int>(type: "int", nullable: false)
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
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    MotherboardPowering = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsModular = table.Column<bool>(type: "bit", nullable: false),
                    CPU4PIN = table.Column<bool>(type: "bit", nullable: false),
                    CPU8PIN = table.Column<bool>(type: "bit", nullable: false),
                    FanDiameter = table.Column<int>(type: "int", nullable: false),
                    Certificate80PLUS = table.Column<bool>(type: "bit", nullable: false),
                    PFC = table.Column<bool>(type: "bit", nullable: false)
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
                name: "RAMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemoryType = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    CASLatency = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Timings = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Voltage = table.Column<double>(type: "float", nullable: false),
                    XMP = table.Column<bool>(type: "bit", nullable: false),
                    AMP = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RAMs_Producers_ProducerId",
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
                    MicroScheme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChipProducerId = table.Column<int>(type: "int", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    Factor = table.Column<double>(type: "float", nullable: false),
                    Interface = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolidStateDrives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolidStateDrives_ChipProducers_ChipProducerId",
                        column: x => x.ChipProducerId,
                        principalTable: "ChipProducers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolidStateDrives_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CPUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechProcess = table.Column<int>(type: "int", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaxRamSpeed = table.Column<int>(type: "int", nullable: false),
                    CrystalSerialId = table.Column<int>(type: "int", nullable: false),
                    Socket = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Cores = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    TurboBoostFrequency = table.Column<int>(type: "int", nullable: false),
                    L3Cache = table.Column<int>(type: "int", nullable: false),
                    IntegratedGraphics = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPUs_CrystalSerials_CrystalSerialId",
                        column: x => x.CrystalSerialId,
                        principalTable: "CrystalSerials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GPUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrystalSerialId = table.Column<int>(type: "int", nullable: false),
                    Interface = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    GraphicsProcessor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Frequency = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    VRAM = table.Column<int>(type: "int", nullable: false),
                    VRAMType = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    VRAMFrequency = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    VRAMBusWidth = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    DirectX = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    SLI_Crossfire = table.Column<bool>(type: "bit", nullable: false),
                    AdditionalPower = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    VGA = table.Column<int>(type: "int", nullable: false),
                    DVI = table.Column<int>(type: "int", nullable: false),
                    HDMI = table.Column<int>(type: "int", nullable: false),
                    DisplayPort = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GPUs_CrystalSerials_CrystalSerialId",
                        column: x => x.CrystalSerialId,
                        principalTable: "CrystalSerials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GPUs_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChipProducers_Id",
                table: "ChipProducers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CPUs_CrystalSerialId",
                table: "CPUs",
                column: "CrystalSerialId");

            migrationBuilder.CreateIndex(
                name: "IX_CPUs_Id",
                table: "CPUs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalSerials_ChipProducerId",
                table: "CrystalSerials",
                column: "ChipProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalSerials_Id",
                table: "CrystalSerials",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GPUs_CrystalSerialId",
                table: "GPUs",
                column: "CrystalSerialId");

            migrationBuilder.CreateIndex(
                name: "IX_GPUs_Id",
                table: "GPUs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GPUs_ProducerId",
                table: "GPUs",
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
                name: "IX_Producers_Id",
                table: "Producers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RAMs_Id",
                table: "RAMs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RAMs_ProducerId",
                table: "RAMs",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_SolidStateDrives_ChipProducerId",
                table: "SolidStateDrives",
                column: "ChipProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_SolidStateDrives_Id",
                table: "SolidStateDrives",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SolidStateDrives_ProducerId",
                table: "SolidStateDrives",
                column: "ProducerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CPUs");

            migrationBuilder.DropTable(
                name: "GPUs");

            migrationBuilder.DropTable(
                name: "HardDrives");

            migrationBuilder.DropTable(
                name: "Motherboards");

            migrationBuilder.DropTable(
                name: "PowerSupplies");

            migrationBuilder.DropTable(
                name: "RAMs");

            migrationBuilder.DropTable(
                name: "SolidStateDrives");

            migrationBuilder.DropTable(
                name: "CrystalSerials");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropTable(
                name: "ChipProducers");
        }
    }
}
