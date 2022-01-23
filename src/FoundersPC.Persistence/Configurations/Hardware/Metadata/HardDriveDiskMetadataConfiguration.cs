using FoundersPC.Domain.Entities.Hardware.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Hardware.Metadata;

public class HardDriveDiskMetadataConfiguration : IEntityTypeConfiguration<HardDriveDiskMetadata>
{
    public void Configure(EntityTypeBuilder<HardDriveDiskMetadata> builder)
    {
        builder.Property(x => x.DiskFactorId)
               .HasColumnName("DiskFactorId")
               .HasColumnType("int")
               .IsRequired(false);

        builder.Property(x => x.DiskConnectionInterfaceId)
               .HasColumnType("int")
               .HasColumnName("DiskConnectionInterfaceId")
               .IsRequired(false);

        builder.Property(x => x.Volume)
               .HasColumnType("int")
               .HasColumnName("Volume")
               .IsRequired(false);

        builder.Property(x => x.BufferSize)
               .HasColumnType("int")
               .HasColumnName("BufferSize")
               .IsRequired(false);

        builder.Property(x => x.HeadSpeed)
               .HasColumnType("int")
               .HasColumnName("HeadSpeed")
               .IsRequired(false);

        builder.Property(x => x.Noise)
               .HasColumnType("float")
               .HasColumnName("Noise")
               .IsRequired(false);

        builder.HasOne(x => x.Factor)
               .WithMany()
               .HasForeignKey(x => x.DiskFactorId)
               .IsRequired(false);

        builder.HasOne(x => x.DiskConnectionInterface)
               .WithMany()
               .HasForeignKey(x => x.DiskConnectionInterfaceId)
               .IsRequired(false);

        builder.HasOne(x => x.HardDriveDisk)
               .WithOne(x => x.Metadata)
               .HasForeignKey<HardDriveDiskMetadata>(x => x.Id)
               .IsRequired();
    }
}