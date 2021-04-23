﻿// <auto-generated />
using System;
using FoundersPC.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoundersPC.API.Infrastructure.Migrations
{
    [DbContext(typeof(FoundersPCHardwareContext))]
    [Migration("20210225164909_RemoveMaxVarchar")]
    partial class RemoveMaxVarchar
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.CaseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Color");

                    b.Property<int?>("Depth")
                        .HasColumnType("int")
                        .HasColumnName("Depth");

                    b.Property<int?>("Height")
                        .HasColumnType("int")
                        .HasColumnName("Height");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Material");

                    b.Property<string>("MaxMotherboardSize")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("MaxMotherboardSize");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Title");

                    b.Property<bool>("TransparentWindow")
                        .HasColumnType("bit")
                        .HasColumnName("TransparentWindow");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("Type");

                    b.Property<double?>("Weight")
                        .HasColumnType("float")
                        .HasColumnName("Weight");

                    b.Property<int?>("Width")
                        .HasColumnType("int")
                        .HasColumnName("Width");

                    b.Property<string>("WindowMaterial")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("WindowMaterial");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.Memory.HardDriveDiskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BufferSize")
                        .HasColumnType("int")
                        .HasColumnName("BufferSize");

                    b.Property<double>("Factor")
                        .HasColumnType("float")
                        .HasColumnName("Factor");

                    b.Property<int>("HeadSpeed")
                        .HasColumnType("int")
                        .HasColumnName("HeadSpeed");

                    b.Property<string>("Interface")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Interface");

                    b.Property<int>("Noise")
                        .HasColumnType("int")
                        .HasColumnName("Noise");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Title");

                    b.Property<int>("Volume")
                        .HasColumnType("int")
                        .HasColumnName("Volume");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("HardDrives");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.Memory.RandomAccessMemoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CASLatency")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("CASLatency");

                    b.Property<bool>("ECC")
                        .HasColumnType("bit")
                        .HasColumnName("ECC");

                    b.Property<int>("Frequency")
                        .HasColumnType("int")
                        .HasColumnName("Frequency");

                    b.Property<string>("MemoryType")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("MemoryType");

                    b.Property<int>("PCIndex")
                        .HasColumnType("int")
                        .HasColumnName("PCIndex");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<string>("Timings")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("Timings");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Title");

                    b.Property<double>("Voltage")
                        .HasColumnType("float")
                        .HasColumnName("Voltage");

                    b.Property<bool>("XMP")
                        .HasColumnType("bit")
                        .HasColumnName("XMP");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("RandomAccessMemoryEntity");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.Memory.SolidStateDriveEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Factor")
                        .HasColumnType("float")
                        .HasColumnName("Factor");

                    b.Property<string>("Interface")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Interface");

                    b.Property<string>("MicroScheme")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("MicroScheme");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<int>("SequentialRead")
                        .HasColumnType("int")
                        .HasColumnName("SequentialRead");

                    b.Property<int>("SequentialRecording")
                        .HasColumnType("int")
                        .HasColumnName("SequentialRecording");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Title");

                    b.Property<int>("Volume")
                        .HasColumnType("int")
                        .HasColumnName("Volume");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("SolidStateDrives");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.MotherboardEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AudioSupport")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("AudioSupport");

                    b.Property<string>("Factor")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Factor");

                    b.Property<int>("M2SlotsCount")
                        .HasColumnType("int")
                        .HasColumnName("M2SlotsCount");

                    b.Property<string>("PCIExpressVersion")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("PCIExpressVersion");

                    b.Property<bool>("PS2Support")
                        .HasColumnType("bit")
                        .HasColumnName("PS2Support");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<string>("RAMMode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("RAMMode");

                    b.Property<int>("RAMSlots")
                        .HasColumnType("int")
                        .HasColumnName("RAMSlots");

                    b.Property<string>("RAMSupport")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)")
                        .HasColumnName("RAMSupport");

                    b.Property<bool>("SLIOrCrossfire")
                        .HasColumnType("bit")
                        .HasColumnName("SLI_Crossfire");

                    b.Property<string>("Socket")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Socket");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Title");

                    b.Property<bool>("WiFiSupport")
                        .HasColumnType("bit")
                        .HasColumnName("WiFiSupport");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("Motherboards");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.PowerSupplyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("CPU4PIN")
                        .HasColumnType("bit")
                        .HasColumnName("CPU4PIN");

                    b.Property<bool?>("CPU8PIN")
                        .HasColumnType("bit")
                        .HasColumnName("CPU8PIN");

                    b.Property<bool>("Certificate80PLUS")
                        .HasColumnType("bit")
                        .HasColumnName("Certificate80PLUS");

                    b.Property<int?>("Efficiency")
                        .HasColumnType("int")
                        .HasColumnName("Efficiency");

                    b.Property<int>("FanDiameter")
                        .HasColumnType("int")
                        .HasColumnName("FanDiameter");

                    b.Property<bool>("IsModular")
                        .HasColumnType("bit")
                        .HasColumnName("IsModular");

                    b.Property<string>("MotherboardPowering")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("MotherboardPowering");

                    b.Property<bool>("PFC")
                        .HasColumnType("bit")
                        .HasColumnName("PFC");

                    b.Property<int>("Power")
                        .HasColumnType("int")
                        .HasColumnName("Power");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("PowerSupplies");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.ProcessorEntity.ProcessorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cores")
                        .HasColumnType("int")
                        .HasColumnName("Cores");

                    b.Property<int>("Frequency")
                        .HasColumnType("int")
                        .HasColumnName("Frequency");

                    b.Property<bool>("IntegratedGraphics")
                        .HasColumnType("bit")
                        .HasColumnName("IntegratedGraphics");

                    b.Property<int>("L1Cache")
                        .HasColumnType("int")
                        .HasColumnName("L1Cache");

                    b.Property<int>("L2Cache")
                        .HasColumnType("int")
                        .HasColumnName("L2Cache");

                    b.Property<int>("L3Cache")
                        .HasColumnType("int")
                        .HasColumnName("L3Cache");

                    b.Property<int>("MaxRamSpeed")
                        .HasColumnType("int")
                        .HasColumnName("MaxRamSpeed");

                    b.Property<int>("ProcessorCoreId")
                        .HasColumnType("int")
                        .HasColumnName("ProcessorCoreId");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<string>("Series")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("Series");

                    b.Property<int>("TDP")
                        .HasColumnType("int")
                        .HasColumnName("TDP");

                    b.Property<int>("TechProcess")
                        .HasColumnType("int")
                        .HasColumnName("TechProcess");

                    b.Property<int>("Threads")
                        .HasColumnType("int")
                        .HasColumnName("Threads");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Title");

                    b.Property<int>("TurboBoostFrequency")
                        .HasColumnType("int")
                        .HasColumnName("TurboBoostFrequency");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProcessorCoreId");

                    b.HasIndex("ProducerId");

                    b.ToTable("Processors");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.ProcessorEntity.ProcessorCore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("L2CachePerCore")
                        .HasColumnType("int")
                        .HasColumnName("L2CachePerCore");

                    b.Property<int>("L3CachePerCore")
                        .HasColumnType("int")
                        .HasColumnName("L3CachePerCore");

                    b.Property<DateTime?>("MarketLaunch")
                        .HasColumnType("datetime2")
                        .HasColumnName("MarketLaunch");

                    b.Property<string>("MicroArchitecture")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("MicroArchitecture");

                    b.Property<string>("Socket")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Socket");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("ProcessorCores");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.ProducerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Country");

                    b.Property<DateTime?>("FoundationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("FoundationDate");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("FullName");

                    b.Property<string>("ShortName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("ShortName");

                    b.Property<string>("Website")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Website");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.VideoCard.VideoCardEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdditionalPower")
                        .HasColumnType("int")
                        .HasColumnName("AdditionalPower");

                    b.Property<int>("DVI")
                        .HasColumnType("int")
                        .HasColumnName("DVI");

                    b.Property<int>("DisplayPort")
                        .HasColumnType("int")
                        .HasColumnName("DisplayPort");

                    b.Property<int>("Frequency")
                        .HasColumnType("int")
                        .HasColumnName("Frequency");

                    b.Property<int>("GraphicsProcessorId")
                        .HasMaxLength(20)
                        .HasColumnType("int")
                        .HasColumnName("GraphicsProcessorId");

                    b.Property<int>("HDMI")
                        .HasColumnType("int")
                        .HasColumnName("HDMI");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<string>("Series")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("Series");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Title");

                    b.Property<int>("VGA")
                        .HasColumnType("int")
                        .HasColumnName("VGA");

                    b.Property<int>("VideoMemoryBusWidth")
                        .HasColumnType("int")
                        .HasColumnName("VideoMemoryBusWidth");

                    b.Property<int>("VideoMemoryFrequency")
                        .HasColumnType("int")
                        .HasColumnName("VideoMemoryFrequency");

                    b.Property<string>("VideoMemoryType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("VideoMemoryType");

                    b.Property<int>("VideoMemoryVolume")
                        .HasColumnType("int")
                        .HasColumnName("VideoMemoryVolume");

                    b.HasKey("Id");

                    b.HasIndex("GraphicsProcessorId");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("VideoCards");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.VideoCard.VideoCardCoreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArchitectureTitle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("ArchitectureTitle");

                    b.Property<string>("Codename")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("Codename");

                    b.Property<string>("ConnectionInterface")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("ConnectionInterface");

                    b.Property<string>("DirectX")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("DirectX_Version");

                    b.Property<string>("MaxResolution")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("MaxResolution");

                    b.Property<int>("MonitorsSupport")
                        .HasColumnType("int")
                        .HasColumnName("MonitorsSupport");

                    b.Property<bool>("SLIOrCrossfire")
                        .HasColumnType("bit")
                        .HasColumnName("SLI_Crossfire");

                    b.Property<int>("TechProcess")
                        .HasColumnType("int")
                        .HasColumnName("TechProcess");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("VideoCardCores");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.CaseEntity", b =>
                {
                    b.HasOne("FoundersPC.API.Domain.Entities.Hardware.ProducerEntity", "ProducerEntity")
                        .WithMany("Cases")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProducerEntity");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.Memory.HardDriveDiskEntity", b =>
                {
                    b.HasOne("FoundersPC.API.Domain.Entities.Hardware.ProducerEntity", "ProducerEntity")
                        .WithMany("HardDrives")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProducerEntity");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.Memory.RandomAccessMemoryEntity", b =>
                {
                    b.HasOne("FoundersPC.API.Domain.Entities.Hardware.ProducerEntity", "ProducerEntity")
                        .WithMany("RandomAccessMemoryEntity")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProducerEntity");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.Memory.SolidStateDriveEntity", b =>
                {
                    b.HasOne("FoundersPC.API.Domain.Entities.Hardware.ProducerEntity", "ProducerEntity")
                        .WithMany("SolidStateDriveEntity")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProducerEntity");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.MotherboardEntity", b =>
                {
                    b.HasOne("FoundersPC.API.Domain.Entities.Hardware.ProducerEntity", "ProducerEntity")
                        .WithMany("Motherboards")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProducerEntity");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.PowerSupplyEntity", b =>
                {
                    b.HasOne("FoundersPC.API.Domain.Entities.Hardware.ProducerEntity", "ProducerEntity")
                        .WithMany("PowerSupplies")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProducerEntity");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.ProcessorEntity.ProcessorEntity", b =>
                {
                    b.HasOne("FoundersPC.API.Domain.Entities.Hardware.ProcessorEntity.ProcessorCore", "CoreEntity")
                        .WithMany("Processors")
                        .HasForeignKey("ProcessorCoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoundersPC.API.Domain.Entities.Hardware.ProducerEntity", "ProducerEntity")
                        .WithMany("Processors")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoreEntity");

                    b.Navigation("ProducerEntity");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.VideoCard.VideoCardEntity", b =>
                {
                    b.HasOne("FoundersPC.API.Domain.Entities.Hardware.VideoCard.VideoCardCoreEntity", "CoreEntity")
                        .WithMany("VideoCards")
                        .HasForeignKey("GraphicsProcessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoundersPC.API.Domain.Entities.Hardware.ProducerEntity", "ProducerEntity")
                        .WithMany("VideoCards")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoreEntity");

                    b.Navigation("ProducerEntity");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.ProcessorEntity.ProcessorCore", b =>
                {
                    b.Navigation("Processors");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.ProducerEntity", b =>
                {
                    b.Navigation("Cases");

                    b.Navigation("HardDrives");

                    b.Navigation("Motherboards");

                    b.Navigation("PowerSupplies");

                    b.Navigation("Processors");

                    b.Navigation("RandomAccessMemoryEntity");

                    b.Navigation("SolidStateDriveEntity");

                    b.Navigation("VideoCards");
                });

            modelBuilder.Entity("FoundersPC.API.Domain.Entities.Hardware.VideoCard.VideoCardCoreEntity", b =>
                {
                    b.Navigation("VideoCards");
                });
#pragma warning restore 612, 618
        }
    }
}
