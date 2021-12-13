using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Domain.Enums;
using FoundersPC.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Hardware.Metadata;

public class HardwareMetadataConfiguration : IEntityTypeConfiguration<HardwareMetadata>
{
    public void Configure(EntityTypeBuilder<HardwareMetadata> builder)
    {
        builder.ToTable("HardwareMetadata");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProducerId)
               .HasColumnType("int")
               .HasColumnName("ProducerId")
               .IsRequired();

        builder.Property(x => x.Title)
               .HasColumnType("nvarchar")
               .HasColumnName("Title")
               .HasMaxLength(512)
               .IsRequired();

        builder.Property(x => x.HardwareTypeId)
               .HasColumnType("int")
               .HasColumnName("HardwareTypeId")
               .IsRequired();

        builder.HasOne(x => x.Producer)
               .WithMany()
               .HasForeignKey(x => x.ProducerId)
               .IsRequired();

        builder.HasOne(x => x.Hardware)
               .WithOne(x => x.BaseMetadata)
               .HasForeignKey<HardwareMetadata>(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.HardwareType)
               .WithMany()
               .HasForeignKey(x => x.HardwareTypeId)
               .IsRequired();

        builder.HasDiscriminator<int>("HardwareTypeId")
               .HasValue<CaseMetadata>((int)HardwareType.Case)
               .HasValue<ProcessorMetadata>((int)HardwareType.CPU)
               .HasValue<VideoCardMetadata>((int)HardwareType.GPU)
               .HasValue<RandomAccessMemoryMetadata>((int)HardwareType.RAM)
               .HasValue<MotherboardMetadata>((int)HardwareType.Motherboard)
               .HasValue<PowerSupplyMetadata>((int)HardwareType.PowerSupply)
               .HasValue<HardDriveDiskMetadata>((int)HardwareType.HardDriveDisk)
               .HasValue<SolidStateDriveMetadata>((int)HardwareType.SolidStateDrive)
               .IsComplete();

        builder.AddAuditableColumns();
    }
}