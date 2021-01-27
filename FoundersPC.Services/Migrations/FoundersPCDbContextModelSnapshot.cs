﻿// <auto-generated />
using System;
using FoundersPC.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoundersPC.Services.Migrations
{
    [DbContext(typeof(FoundersPCDbContext))]
    partial class FoundersPCDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("FoundersPC.Services.Models.CPU", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<int>("Cores")
                        .HasColumnType("int")
                        .HasColumnName("Cores");

                    b.Property<int>("CrystalSerialId")
                        .HasColumnType("int")
                        .HasColumnName("CrystalSerialId");

                    b.Property<int>("Frequency")
                        .HasColumnType("int")
                        .HasColumnName("Frequency");

                    b.Property<bool>("IntegratedGraphics")
                        .HasColumnType("bit")
                        .HasColumnName("IntegratedGraphics");

                    b.Property<int>("L3Cache")
                        .HasColumnType("int")
                        .HasColumnName("L3Cache");

                    b.Property<int>("MaxRamSpeed")
                        .HasColumnType("int")
                        .HasColumnName("MaxRamSpeed");

                    b.Property<string>("Series")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Series");

                    b.Property<string>("Socket")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Socket");

                    b.Property<int>("TechProcess")
                        .HasColumnType("int")
                        .HasColumnName("TechProcess");

                    b.Property<int>("TurboBoostFrequency")
                        .HasColumnType("int")
                        .HasColumnName("TurboBoostFrequency");

                    b.HasKey("Id");

                    b.HasIndex("CrystalSerialId");

                    b.HasIndex("Id");

                    b.ToTable("CPUs");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.ChipProducer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Country");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("ChipProducers");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.CrystalSerial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<int>("ChipProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ChipProducerId");

                    b.Property<string>("MicroArchitecture")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("MicroArchitecture");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("ChipProducerId");

                    b.HasIndex("Id");

                    b.ToTable("CrystalSerials");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.GPU", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<int>("AdditionalPower")
                        .HasColumnType("int")
                        .HasColumnName("AdditionalPower");

                    b.Property<int>("CrystalSerialId")
                        .HasColumnType("int")
                        .HasColumnName("CrystalSerialId");

                    b.Property<int>("DVI")
                        .HasColumnType("int")
                        .HasColumnName("DVI");

                    b.Property<int>("DirectX")
                        .HasMaxLength(3)
                        .HasColumnType("int")
                        .HasColumnName("DirectX");

                    b.Property<int>("DisplayPort")
                        .HasColumnType("int")
                        .HasColumnName("DisplayPort");

                    b.Property<int>("Frequency")
                        .HasMaxLength(5)
                        .HasColumnType("int")
                        .HasColumnName("Frequency");

                    b.Property<string>("GraphicsProcessor")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("GraphicsProcessor");

                    b.Property<int>("HDMI")
                        .HasColumnType("int")
                        .HasColumnName("HDMI");

                    b.Property<string>("Interface")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("Interface");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<bool>("SLIOrCrossfire")
                        .HasColumnType("bit")
                        .HasColumnName("SLI_Crossfire");

                    b.Property<int>("VGA")
                        .HasColumnType("int")
                        .HasColumnName("VGA");

                    b.Property<int>("VRAM")
                        .HasColumnType("int")
                        .HasColumnName("VRAM");

                    b.Property<int>("VRAMBusWidth")
                        .HasMaxLength(4)
                        .HasColumnType("int")
                        .HasColumnName("VRAMBusWidth");

                    b.Property<int>("VRAMFrequency")
                        .HasMaxLength(5)
                        .HasColumnType("int")
                        .HasColumnName("VRAMFrequency");

                    b.Property<string>("VRAMType")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)")
                        .HasColumnName("VRAMType");

                    b.Property<int>("Width")
                        .HasColumnType("int")
                        .HasColumnName("Width");

                    b.HasKey("Id");

                    b.HasIndex("CrystalSerialId");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("GPUs");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.HDD", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

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
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Interface");

                    b.Property<int>("Noise")
                        .HasColumnType("int")
                        .HasColumnName("Noise");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<int>("Volume")
                        .HasColumnType("int")
                        .HasColumnName("Volume");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("HardDrives");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.Motherboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<string>("AudioSupport")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("AudioSupport");

                    b.Property<double>("Factor")
                        .HasColumnType("float")
                        .HasColumnName("Factor");

                    b.Property<int>("M2SlotsCount")
                        .HasColumnType("int")
                        .HasColumnName("M2SlotsCount");

                    b.Property<bool>("PS2Support")
                        .HasColumnType("bit")
                        .HasColumnName("PS2Support");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<string>("RAMMode")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)")
                        .HasColumnName("RAMMode");

                    b.Property<int>("RAMSlots")
                        .HasColumnType("int")
                        .HasColumnName("RAMSlots");

                    b.Property<string>("RAMSupport")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("RAMSupport");

                    b.Property<bool>("SLIOrCrossfire")
                        .HasColumnType("bit")
                        .HasColumnName("SLI_Crossfire");

                    b.Property<string>("Socket")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Socket");

                    b.Property<bool>("WiFiSupport")
                        .HasColumnType("bit")
                        .HasColumnName("WiFiSupport");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("Motherboards");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.PowerSupply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<bool>("CPU4PIN")
                        .HasColumnType("bit")
                        .HasColumnName("CPU4PIN");

                    b.Property<bool>("CPU8PIN")
                        .HasColumnType("bit")
                        .HasColumnName("CPU8PIN");

                    b.Property<bool>("Certificate80PLUS")
                        .HasColumnType("bit")
                        .HasColumnName("Certificate80PLUS");

                    b.Property<int>("FanDiameter")
                        .HasColumnType("int")
                        .HasColumnName("FanDiameter");

                    b.Property<bool>("IsModular")
                        .HasColumnType("bit")
                        .HasColumnName("IsModular");

                    b.Property<string>("MotherboardPowering")
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

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("PowerSupplies");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.Producer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Country");

                    b.Property<DateTime?>("FoundationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("FoundationDate");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("FullName");

                    b.Property<string>("ShortName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("ShortName");

                    b.Property<string>("Website")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Country");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.RAM", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<bool>("AMP")
                        .HasColumnType("bit")
                        .HasColumnName("AMP");

                    b.Property<string>("CASLatency")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("CASLatency");

                    b.Property<int>("Frequency")
                        .HasColumnType("int")
                        .HasColumnName("Frequency");

                    b.Property<string>("MemoryType")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("MemoryType");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<string>("Timings")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("Timings");

                    b.Property<double>("Voltage")
                        .HasColumnType("float")
                        .HasColumnName("Voltage");

                    b.Property<int>("Volume")
                        .HasColumnType("int")
                        .HasColumnName("Volume");

                    b.Property<bool>("XMP")
                        .HasColumnType("bit")
                        .HasColumnName("XMP");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("RAMs");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.SSD", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<int>("ChipProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ChipProducerId");

                    b.Property<double>("Factor")
                        .HasColumnType("float")
                        .HasColumnName("Factor");

                    b.Property<string>("Interface")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Interface");

                    b.Property<string>("MicroScheme")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("MicroScheme");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    b.Property<int>("Volume")
                        .HasColumnType("int")
                        .HasColumnName("Volume");

                    b.HasKey("Id");

                    b.HasIndex("ChipProducerId");

                    b.HasIndex("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("SolidStateDrives");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.CPU", b =>
                {
                    b.HasOne("FoundersPC.Services.Models.CrystalSerial", "CrystalSerial")
                        .WithMany("CPUs")
                        .HasForeignKey("CrystalSerialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CrystalSerial");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.CrystalSerial", b =>
                {
                    b.HasOne("FoundersPC.Services.Models.ChipProducer", "ChipProducer")
                        .WithMany("CrystalSerials")
                        .HasForeignKey("ChipProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChipProducer");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.GPU", b =>
                {
                    b.HasOne("FoundersPC.Services.Models.CrystalSerial", "CrystalSerial")
                        .WithMany("GPUs")
                        .HasForeignKey("CrystalSerialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoundersPC.Services.Models.Producer", "Producer")
                        .WithMany("GPUs")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CrystalSerial");

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.HDD", b =>
                {
                    b.HasOne("FoundersPC.Services.Models.Producer", "Producer")
                        .WithMany("HDDs")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.Motherboard", b =>
                {
                    b.HasOne("FoundersPC.Services.Models.Producer", "Producer")
                        .WithMany("Motherboards")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.PowerSupply", b =>
                {
                    b.HasOne("FoundersPC.Services.Models.Producer", "Producer")
                        .WithMany("PowerSupplies")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.RAM", b =>
                {
                    b.HasOne("FoundersPC.Services.Models.Producer", "Producer")
                        .WithMany("RAMs")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.SSD", b =>
                {
                    b.HasOne("FoundersPC.Services.Models.ChipProducer", "ChipsProducer")
                        .WithMany("SSDs")
                        .HasForeignKey("ChipProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoundersPC.Services.Models.Producer", "Producer")
                        .WithMany("SSDs")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChipsProducer");

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.ChipProducer", b =>
                {
                    b.Navigation("CrystalSerials");

                    b.Navigation("SSDs");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.CrystalSerial", b =>
                {
                    b.Navigation("CPUs");

                    b.Navigation("GPUs");
                });

            modelBuilder.Entity("FoundersPC.Services.Models.Producer", b =>
                {
                    b.Navigation("GPUs");

                    b.Navigation("HDDs");

                    b.Navigation("Motherboards");

                    b.Navigation("PowerSupplies");

                    b.Navigation("RAMs");

                    b.Navigation("SSDs");
                });
#pragma warning restore 612, 618
        }
    }
}
