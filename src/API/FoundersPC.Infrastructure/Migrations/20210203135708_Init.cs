#region Using derectives

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace FoundersPC.Infrastructure.Migrations
{
	public partial class Init : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
			                             "ProcessorCores",
			                             table => new
			                                      {
				                                      Id = table.Column<int>("int", nullable:false)
				                                                .Annotation("SqlServer:Identity", "1, 1"),
				                                      MarketLaunch = table.Column<DateTime>("datetime2", nullable:true),
				                                      Title = table.Column<string>("nvarchar(50)", maxLength:50,
					                                      nullable:false),
				                                      MicroArchitecture =
					                                      table.Column<string>("nvarchar(30)", maxLength:30,
					                                                           nullable:false),
				                                      L2CachePerCore = table.Column<int>("int", nullable:false),
				                                      L3CachePerCore = table.Column<int>("int", nullable:false),
				                                      Socket = table.Column<string>("nvarchar(10)", maxLength:10,
					                                      nullable:false)
			                                      },
			                             constraints:table => { table.PrimaryKey("PK_ProcessorCores", x => x.Id); });

			migrationBuilder.CreateTable(
			                             "Producers",
			                             table => new
			                                      {
				                                      Id = table.Column<int>("int", nullable:false)
				                                                .Annotation("SqlServer:Identity", "1, 1"),
				                                      ShortName = table.Column<string>("nvarchar(20)", maxLength:20,
					                                      nullable:true),
				                                      FullName = table.Column<string>("nvarchar(100)", maxLength:100,
					                                      nullable:false),
				                                      Country = table.Column<string>("nvarchar(50)", maxLength:50,
					                                      nullable:true),
				                                      Website = table.Column<string>("nvarchar(100)", maxLength:100,
					                                      nullable:true),
				                                      FoundationDate =
					                                      table.Column<DateTime>("datetime2", nullable:true)
			                                      },
			                             constraints:table => { table.PrimaryKey("PK_Producers", x => x.Id); });

			migrationBuilder.CreateTable(
			                             "VideoCardCores",
			                             table => new
			                                      {
				                                      Id = table.Column<int>("int", nullable:false)
				                                                .Annotation("SqlServer:Identity", "1, 1"),
				                                      Title = table.Column<string>("nvarchar(30)", maxLength:30,
					                                      nullable:true),
				                                      TechProcess = table.Column<int>("int", nullable:false),
				                                      MaxResolution =
					                                      table.Column<string>("nvarchar(20)", maxLength:20,
					                                                           nullable:false),
				                                      MonitorsSupport = table.Column<int>("int", nullable:false),
				                                      Interface = table.Column<string>("nvarchar(30)", maxLength:30,
					                                      nullable:false),
				                                      Frequency = table.Column<int>("int", maxLength:5, nullable:false),
				                                      DirectX_Version =
					                                      table.Column<int>("int", maxLength:3, nullable:false),
				                                      SLI_Crossfire = table.Column<bool>("bit", nullable:false),
				                                      ArchitectureTitle =
					                                      table.Column<string>("nvarchar(max)", nullable:false)
			                                      },
			                             constraints:table => { table.PrimaryKey("PK_VideoCardCores", x => x.Id); });

			migrationBuilder.CreateTable(
			                             "Cases",
			                             table => new
			                                      {
				                                      Id = table.Column<int>("int", nullable:false)
				                                                .Annotation("SqlServer:Identity", "1, 1"),
				                                      Type = table.Column<string>("nvarchar(3)", maxLength:3,
					                                      nullable:false),
				                                      MaxMotherboardSize =
					                                      table.Column<string>("nvarchar(20)", maxLength:20,
					                                                           nullable:false),
				                                      Material = table.Column<string>("nvarchar(50)", maxLength:50,
					                                      nullable:false),
				                                      WindowMaterial =
					                                      table.Column<string>("nvarchar(50)", maxLength:50,
					                                                           nullable:false),
				                                      TransparentWindow = table.Column<bool>("bit", nullable:false),
				                                      Color = table.Column<string>("nvarchar(50)", maxLength:50,
					                                      nullable:false),
				                                      ProducerId = table.Column<int>("int", nullable:false),
				                                      MarketLaunch = table.Column<DateTime>("datetime2", nullable:true)
			                                      },
			                             constraints:table =>
			                                         {
				                                         table.PrimaryKey("PK_Cases", x => x.Id);
				                                         table.ForeignKey(
				                                                          "FK_Cases_Producers_ProducerId",
				                                                          x => x.ProducerId,
				                                                          "Producers",
				                                                          "Id",
				                                                          onDelete:ReferentialAction.Cascade);
			                                         });

			migrationBuilder.CreateTable(
			                             "HardDrives",
			                             table => new
			                                      {
				                                      Id = table.Column<int>("int", nullable:false)
				                                                .Annotation("SqlServer:Identity", "1, 1"),
				                                      HeadSpeed = table.Column<int>("int", nullable:false),
				                                      BufferSize = table.Column<int>("int", nullable:false),
				                                      Noise = table.Column<int>("int", nullable:false),
				                                      ProducerId = table.Column<int>("int", nullable:false),
				                                      MarketLaunch = table.Column<DateTime>("datetime2", nullable:true),
				                                      Factor = table.Column<double>("float", nullable:false),
				                                      Interface = table.Column<string>("nvarchar(20)", maxLength:20,
					                                      nullable:false),
				                                      Volume = table.Column<int>("int", nullable:false)
			                                      },
			                             constraints:table =>
			                                         {
				                                         table.PrimaryKey("PK_HardDrives", x => x.Id);
				                                         table.ForeignKey(
				                                                          "FK_HardDrives_Producers_ProducerId",
				                                                          x => x.ProducerId,
				                                                          "Producers",
				                                                          "Id",
				                                                          onDelete:ReferentialAction.Cascade);
			                                         });

			migrationBuilder.CreateTable(
			                             "Motherboards",
			                             table => new
			                                      {
				                                      Id = table.Column<int>("int", nullable:false)
				                                                .Annotation("SqlServer:Identity", "1, 1"),
				                                      Socket = table.Column<string>("nvarchar(10)", maxLength:10,
					                                      nullable:false),
				                                      Factor = table.Column<double>("float", nullable:false),
				                                      RAMSupport =
					                                      table.Column<string>("nvarchar(6)", maxLength:6,
					                                                           nullable:false),
				                                      RAMSlots = table.Column<int>("int", nullable:false),
				                                      RAMMode = table.Column<string>("nvarchar(2)", maxLength:2,
					                                      nullable:false),
				                                      SLI_Crossfire = table.Column<bool>("bit", nullable:false),
				                                      AudioSupport =
					                                      table.Column<string>("nvarchar(20)", maxLength:20,
					                                                           nullable:false),
				                                      WiFiSupport = table.Column<bool>("bit", nullable:false),
				                                      PS2Support = table.Column<bool>("bit", nullable:false),
				                                      M2SlotsCount = table.Column<int>("int", nullable:false),
				                                      PCIExpressVersion =
					                                      table.Column<string>("nvarchar(12)", maxLength:12,
					                                                           nullable:false),
				                                      ProducerId = table.Column<int>("int", nullable:false),
				                                      MarketLaunch = table.Column<DateTime>("datetime2", nullable:true)
			                                      },
			                             constraints:table =>
			                                         {
				                                         table.PrimaryKey("PK_Motherboards", x => x.Id);
				                                         table.ForeignKey(
				                                                          "FK_Motherboards_Producers_ProducerId",
				                                                          x => x.ProducerId,
				                                                          "Producers",
				                                                          "Id",
				                                                          onDelete:ReferentialAction.Cascade);
			                                         });

			migrationBuilder.CreateTable(
			                             "PowerSupplies",
			                             table => new
			                                      {
				                                      Id = table.Column<int>("int", nullable:false)
				                                                .Annotation("SqlServer:Identity", "1, 1"),
				                                      Power = table.Column<int>("int", nullable:false),
				                                      Efficiency = table.Column<int>("int", nullable:false),
				                                      MotherboardPowering =
					                                      table.Column<string>("nvarchar(10)", maxLength:10,
					                                                           nullable:false),
				                                      IsModular = table.Column<bool>("bit", nullable:false),
				                                      CPU4PIN = table.Column<bool>("bit", nullable:false),
				                                      CPU8PIN = table.Column<bool>("bit", nullable:false),
				                                      FanDiameter = table.Column<int>("int", nullable:false),
				                                      Certificate80PLUS = table.Column<bool>("bit", nullable:false),
				                                      PFC = table.Column<bool>("bit", nullable:false),
				                                      ProducerId = table.Column<int>("int", nullable:false),
				                                      MarketLaunch = table.Column<DateTime>("datetime2", nullable:true)
			                                      },
			                             constraints:table =>
			                                         {
				                                         table.PrimaryKey("PK_PowerSupplies", x => x.Id);
				                                         table.ForeignKey(
				                                                          "FK_PowerSupplies_Producers_ProducerId",
				                                                          x => x.ProducerId,
				                                                          "Producers",
				                                                          "Id",
				                                                          onDelete:ReferentialAction.Cascade);
			                                         });

			migrationBuilder.CreateTable(
			                             "Processors",
			                             table => new
			                                      {
				                                      Id = table.Column<int>("int", nullable:false)
				                                                .Annotation("SqlServer:Identity", "1, 1"),
				                                      TDP = table.Column<int>("int", nullable:false),
				                                      ProcessorCoreId = table.Column<int>("int", nullable:false),
				                                      Name = table.Column<string>("nvarchar(20)", maxLength:20,
					                                      nullable:false),
				                                      MaxRamSpeed = table.Column<int>("int", nullable:false),
				                                      Cores = table.Column<int>("int", nullable:false),
				                                      Threads = table.Column<int>("int", nullable:false),
				                                      Frequency = table.Column<int>("int", nullable:false),
				                                      TurboBoostFrequency = table.Column<int>("int", nullable:false),
				                                      TechProcess = table.Column<int>("int", nullable:false),
				                                      L1Cache = table.Column<int>("int", nullable:false),
				                                      L2Cache = table.Column<int>("int", nullable:false),
				                                      L3Cache = table.Column<int>("int", nullable:false),
				                                      IntegratedGraphics = table.Column<bool>("bit", nullable:false),
				                                      ProducerId = table.Column<int>("int", nullable:false),
				                                      MarketLaunch = table.Column<DateTime>("datetime2", nullable:true)
			                                      },
			                             constraints:table =>
			                                         {
				                                         table.PrimaryKey("PK_Processors", x => x.Id);
				                                         table.ForeignKey(
				                                                          "FK_Processors_ProcessorCores_ProcessorCoreId",
				                                                          x => x.ProcessorCoreId,
				                                                          "ProcessorCores",
				                                                          "Id",
				                                                          onDelete:ReferentialAction.Cascade);

				                                         table.ForeignKey(
				                                                          "FK_Processors_Producers_ProducerId",
				                                                          x => x.ProducerId,
				                                                          "Producers",
				                                                          "Id",
				                                                          onDelete:ReferentialAction.Cascade);
			                                         });

			migrationBuilder.CreateTable(
			                             "RandomAccessMemory",
			                             table => new
			                                      {
				                                      Id = table.Column<int>("int", nullable:false)
				                                                .Annotation("SqlServer:Identity", "1, 1"),
				                                      MemoryType =
					                                      table.Column<string>("nvarchar(15)", maxLength:15,
					                                                           nullable:false),
				                                      Frequency = table.Column<int>("int", nullable:false),
				                                      CASLatency =
					                                      table.Column<string>("nvarchar(5)", maxLength:5,
					                                                           nullable:false),
				                                      Timings = table.Column<string>("nvarchar(8)", maxLength:8,
					                                      nullable:false),
				                                      Voltage = table.Column<double>("float", nullable:false),
				                                      XMP = table.Column<bool>("bit", nullable:false),
				                                      ECC = table.Column<bool>("bit", nullable:false),
				                                      PCIndex = table.Column<int>("int", nullable:false),
				                                      ProducerId = table.Column<int>("int", nullable:false),
				                                      MarketLaunch = table.Column<DateTime>("datetime2", nullable:true)
			                                      },
			                             constraints:table =>
			                                         {
				                                         table.PrimaryKey("PK_RandomAccessMemory", x => x.Id);
				                                         table.ForeignKey(
				                                                          "FK_RandomAccessMemory_Producers_ProducerId",
				                                                          x => x.ProducerId,
				                                                          "Producers",
				                                                          "Id",
				                                                          onDelete:ReferentialAction.Cascade);
			                                         });

			migrationBuilder.CreateTable(
			                             "SolidStateDrives",
			                             table => new
			                                      {
				                                      Id = table.Column<int>("int", nullable:false)
				                                                .Annotation("SqlServer:Identity", "1, 1"),
				                                      MicroScheme =
					                                      table.Column<string>("nvarchar(50)", maxLength:50,
					                                                           nullable:false),
				                                      SequentialRead = table.Column<int>("int", nullable:false),
				                                      SequentialRecording = table.Column<int>("int", nullable:false),
				                                      ProducerId = table.Column<int>("int", nullable:false),
				                                      MarketLaunch = table.Column<DateTime>("datetime2", nullable:true),
				                                      Factor = table.Column<double>("float", nullable:false),
				                                      Interface = table.Column<string>("nvarchar(20)", maxLength:20,
					                                      nullable:false),
				                                      Volume = table.Column<int>("int", nullable:false)
			                                      },
			                             constraints:table =>
			                                         {
				                                         table.PrimaryKey("PK_SolidStateDrives", x => x.Id);
				                                         table.ForeignKey(
				                                                          "FK_SolidStateDrives_Producers_ProducerId",
				                                                          x => x.ProducerId,
				                                                          "Producers",
				                                                          "Id",
				                                                          onDelete:ReferentialAction.Cascade);
			                                         });

			migrationBuilder.CreateTable(
			                             "VideoCards",
			                             table => new
			                                      {
				                                      Id = table.Column<int>("int", nullable:false)
				                                                .Annotation("SqlServer:Identity", "1, 1"),
				                                      GraphicsProcessorId =
					                                      table.Column<int>("int", maxLength:20, nullable:false),
				                                      AdditionalPower = table.Column<int>("int", nullable:false),
				                                      VideoMemoryVolume = table.Column<int>("int", nullable:false),
				                                      VideoMemoryType =
					                                      table.Column<string>("nvarchar(7)", maxLength:7,
					                                                           nullable:false),
				                                      VideoMemoryFrequency =
					                                      table.Column<int>("int", maxLength:5, nullable:false),
				                                      VideoMemoryBusWidth =
					                                      table.Column<int>("int", maxLength:4, nullable:false),
				                                      VGA = table.Column<int>("int", nullable:false),
				                                      DVI = table.Column<int>("int", nullable:false),
				                                      HDMI = table.Column<int>("int", nullable:false),
				                                      DisplayPort = table.Column<int>("int", nullable:false),
				                                      ProducerId = table.Column<int>("int", nullable:false),
				                                      MarketLaunch = table.Column<DateTime>("datetime2", nullable:true)
			                                      },
			                             constraints:table =>
			                                         {
				                                         table.PrimaryKey("PK_VideoCards", x => x.Id);
				                                         table.ForeignKey(
				                                                          "FK_VideoCards_Producers_ProducerId",
				                                                          x => x.ProducerId,
				                                                          "Producers",
				                                                          "Id",
				                                                          onDelete:ReferentialAction.Cascade);

				                                         table.ForeignKey(
				                                                          "FK_VideoCards_VideoCardCores_GraphicsProcessorId",
				                                                          x => x.GraphicsProcessorId,
				                                                          "VideoCardCores",
				                                                          "Id",
				                                                          onDelete:ReferentialAction.Cascade);
			                                         });

			migrationBuilder.CreateIndex(
			                             "IX_Cases_Id",
			                             "Cases",
			                             "Id");

			migrationBuilder.CreateIndex(
			                             "IX_Cases_ProducerId",
			                             "Cases",
			                             "ProducerId");

			migrationBuilder.CreateIndex(
			                             "IX_HardDrives_Id",
			                             "HardDrives",
			                             "Id");

			migrationBuilder.CreateIndex(
			                             "IX_HardDrives_ProducerId",
			                             "HardDrives",
			                             "ProducerId");

			migrationBuilder.CreateIndex(
			                             "IX_Motherboards_Id",
			                             "Motherboards",
			                             "Id");

			migrationBuilder.CreateIndex(
			                             "IX_Motherboards_ProducerId",
			                             "Motherboards",
			                             "ProducerId");

			migrationBuilder.CreateIndex(
			                             "IX_PowerSupplies_Id",
			                             "PowerSupplies",
			                             "Id");

			migrationBuilder.CreateIndex(
			                             "IX_PowerSupplies_ProducerId",
			                             "PowerSupplies",
			                             "ProducerId");

			migrationBuilder.CreateIndex(
			                             "IX_Processors_Id",
			                             "Processors",
			                             "Id");

			migrationBuilder.CreateIndex(
			                             "IX_Processors_ProcessorCoreId",
			                             "Processors",
			                             "ProcessorCoreId");

			migrationBuilder.CreateIndex(
			                             "IX_Processors_ProducerId",
			                             "Processors",
			                             "ProducerId");

			migrationBuilder.CreateIndex(
			                             "IX_Producers_Id",
			                             "Producers",
			                             "Id");

			migrationBuilder.CreateIndex(
			                             "IX_RandomAccessMemory_Id",
			                             "RandomAccessMemory",
			                             "Id");

			migrationBuilder.CreateIndex(
			                             "IX_RandomAccessMemory_ProducerId",
			                             "RandomAccessMemory",
			                             "ProducerId");

			migrationBuilder.CreateIndex(
			                             "IX_SolidStateDrives_Id",
			                             "SolidStateDrives",
			                             "Id");

			migrationBuilder.CreateIndex(
			                             "IX_SolidStateDrives_ProducerId",
			                             "SolidStateDrives",
			                             "ProducerId");

			migrationBuilder.CreateIndex(
			                             "IX_VideoCards_GraphicsProcessorId",
			                             "VideoCards",
			                             "GraphicsProcessorId");

			migrationBuilder.CreateIndex(
			                             "IX_VideoCards_Id",
			                             "VideoCards",
			                             "Id");

			migrationBuilder.CreateIndex(
			                             "IX_VideoCards_ProducerId",
			                             "VideoCards",
			                             "ProducerId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
			                           "Cases");

			migrationBuilder.DropTable(
			                           "HardDrives");

			migrationBuilder.DropTable(
			                           "Motherboards");

			migrationBuilder.DropTable(
			                           "PowerSupplies");

			migrationBuilder.DropTable(
			                           "Processors");

			migrationBuilder.DropTable(
			                           "RandomAccessMemory");

			migrationBuilder.DropTable(
			                           "SolidStateDrives");

			migrationBuilder.DropTable(
			                           "VideoCards");

			migrationBuilder.DropTable(
			                           "ProcessorCores");

			migrationBuilder.DropTable(
			                           "Producers");

			migrationBuilder.DropTable(
			                           "VideoCardCores");
		}
	}
}