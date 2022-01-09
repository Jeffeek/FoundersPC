using System.Data;
using FluentMigrator;
using FluentMigrator.SqlServer;
using FoundersPC.Persistence.Migrations.Common;
using FoundersPC.Persistence.Migrations.Extensions;

namespace FoundersPC.Persistence.Migrations.Migrations._2021._12._18;

[FoundersPCMigration(2021_12_18_3, "Add Hardware Metadata Table")]
public class AddHardwareMetadataTable : Migration
{
    public override void Up()
    {
        //Base
        Create.Table("HardwareMetadata")
              .WithColumn("Id").AsInt32().ForeignKey("FK_HardwareMetadata_Hardware", "Hardware", "Id")
              .AddAuditableColumns("HardwareMetadata")
              .WithColumn("ProducerId").AsInt32().NotNullable().ForeignKey("FK_HardwareMetadata_Producers_ProducerId", "Producers", "Id").OnDeleteOrUpdate(Rule.Cascade)
              .WithColumn("Title").AsString().NotNullable()
              .WithColumn("HardwareTypeId").AsInt32().NotNullable().ForeignKey("FK_HardwareMetadata_HardwareType_HardwareTypeId", "HardwareType", "Id").OnUpdate(Rule.None)
              //Case
              .WithColumn("WindowMaterialId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_WindowMaterialId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("CaseTypeId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_CaseTypeId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("ColorId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_ColorId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("MaxMotherboardSize").AsDouble().Nullable()
              .WithColumn("MaterialId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_MaterialId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("TransparentWindow").AsBoolean().Nullable()
              .WithColumn("Weight").AsDouble().Nullable()
              .WithColumn("Height").AsDouble().Nullable()
              .WithColumn("Width").AsDouble().Nullable()
              .WithColumn("Depth").AsDouble().Nullable()
              //HDD
              .WithColumn("DiskFactorId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_DiskFactorId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("DiskConnectionInterfaceId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_DiskConnectionInterfaceId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("Volume").AsInt32().Nullable()
              .WithColumn("BufferSize").AsInt32().Nullable()
              .WithColumn("HeadSpeed").AsInt32().Nullable()
              .WithColumn("Noise").AsDouble().Nullable()
              //Motherboard
              .WithColumn("SocketId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_SocketId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("MotherboardFactorId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_MotherboardFactorId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("RAMTypeId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_RAMTypeId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("RAMSlotsCount").AsInt32().Nullable()
              .WithColumn("RAMModeId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_RAMModeId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("SliSupport").AsBoolean().Nullable()
              .WithColumn("CrossfireSupport").AsBoolean().Nullable()
              .WithColumn("AudioSupport").AsString().Nullable()
              .WithColumn("WiFiSupport").AsBoolean().Nullable()
              .WithColumn("PS2Support").AsBoolean().Nullable()
              .WithColumn("M2SlotsCount").AsInt32().Nullable()
              .WithColumn("PCIExpressVersion").AsString().Nullable()
              //Power Supply
              .WithColumn("Power").AsInt32().Nullable()
              .WithColumn("Efficiency").AsInt32().Nullable()
              .WithColumn("MotherboardPoweringId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_MotherboardPoweringId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("IsModular").AsBoolean().Nullable()
              .WithColumn("CPU4PIN").AsBoolean().Nullable()
              .WithColumn("CPU8PIN").AsBoolean().Nullable()
              .WithColumn("FanDiameter").AsInt32().Nullable()
              .WithColumn("Certificate80PLUS").AsBoolean().Nullable()
              .WithColumn("PFC").AsBoolean().Nullable()
              //CPU
              .WithColumn("TDP").AsInt32().Nullable()
              .WithColumn("MaxRamSpeed").AsInt32().Nullable()
              .WithColumn("CoresCount").AsInt32().Nullable()
              .WithColumn("ThreadsCount").AsInt32().Nullable()
              .WithColumn("Frequency").AsInt32().Nullable()
              .WithColumn("TurboBoostFrequency").AsInt32().Nullable()
              .WithColumn("TechProcessId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_TechProcessId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("L1Cache").AsInt32().Nullable()
              .WithColumn("L2Cache").AsInt32().Nullable()
              .WithColumn("L3Cache").AsInt32().Nullable()
              .WithColumn("Series").AsString().Nullable()
              .WithColumn("IntegratedGraphicsId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Hardware_IntegratedGraphicsId", "Hardware", "Id").OnDeleteOrUpdate(Rule.None)
              //RAM
              .WithColumn("Timings").AsString().Nullable()
              .WithColumn("Voltage").AsDouble().Nullable()
              .WithColumn("XMP").AsBoolean().Nullable()
              .WithColumn("ECC").AsBoolean().Nullable()
              .WithColumn("PCIndex").AsInt32().Nullable()
              //SSD
              .WithColumn("MicroScheme").AsString(2048).Nullable()
              .WithColumn("SequentialRead").AsDouble().Nullable()
              .WithColumn("SequentialRecording").AsDouble().Nullable()
              //GPU
              .WithColumn("VideoMemoryTypeId").AsInt32().Nullable().ForeignKey("FK_HardwareMetadata_Metadata_VideoMemoryId", "Metadata", "Id").OnUpdate(Rule.None)
              .WithColumn("MemoryFrequency").AsInt32().Nullable()
              .WithColumn("MemoryBusWidth").AsInt32().Nullable()
              .WithColumn("AdditionalPower").AsInt32().Nullable()
              .WithColumn("VGA").AsInt32().Nullable()
              .WithColumn("DVI").AsInt32().Nullable()
              .WithColumn("HDMI").AsInt32().Nullable()
              .WithColumn("DisplayPort").AsInt32().Nullable();

        Create.PrimaryKey("PK_HardwareMetadata").OnTable("HardwareMetadata").Column("Id").Clustered();
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_HardwareMetadata_Hardware_Id")
              .OnTable("HardwareMetadata");

        Delete.Table("HardwareMetadata");
    }
}