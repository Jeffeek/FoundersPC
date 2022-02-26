using FoundersPC.Domain.Entities.Hardware.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Hardware.Metadata;

public class MotherboardMetadataConfiguration : IEntityTypeConfiguration<MotherboardMetadata>
{
    public void Configure(EntityTypeBuilder<MotherboardMetadata> builder)
    {
        builder.Property(x => x.SocketId)
               .HasColumnType("int")
               .HasColumnName("SocketId")
               .IsRequired(false);

        builder.Property(x => x.MotherboardFactorId)
               .HasColumnType("int")
               .HasColumnName("MotherboardFactorId")
               .IsRequired(false);

        builder.Property(x => x.RAMTypeId)
               .HasColumnType("int")
               .HasColumnName("RAMTypeId")
               .IsRequired(false);

        builder.Property(x => x.RAMSlotsCount)
               .HasColumnType("int")
               .HasColumnName("RAMSlotsCount")
               .IsRequired(false);

        builder.Property(x => x.RAMModeId)
               .HasColumnType("int")
               .HasColumnName("RAMModeId")
               .IsRequired(false);

        builder.Property(x => x.SliSupport)
               .HasColumnType("bit")
               .HasColumnName("SliSupport")
               .IsRequired(false);

        builder.Property(x => x.CrossfireSupport)
               .HasColumnType("bit")
               .HasColumnName("CrossfireSupport")
               .IsRequired(false);

        builder.Property(x => x.AudioSupport)
               .HasColumnType("nvarchar")
               .HasColumnName("AudioSupport")
               .HasMaxLength(256)
               .IsRequired(false);

        builder.Property(x => x.WiFiSupport)
               .HasColumnType("bit")
               .HasColumnName("WiFiSupport")
               .IsRequired(false);

        builder.Property(x => x.PS2Support)
               .HasColumnType("bit")
               .HasColumnName("PS2Support")
               .IsRequired(false);

        builder.Property(x => x.M2SlotsCount)
               .HasColumnType("int")
               .HasColumnName("M2SlotsCount")
               .IsRequired(false);

        builder.Property(x => x.PCIExpressVersion)
               .HasColumnType("nvarchar")
               .HasColumnName("PCIExpressVersion")
               .IsRequired(false);

        builder.HasOne(x => x.Socket)
               .WithMany(x => x.Motherboards)
               .HasForeignKey(x => x.SocketId)
               .IsRequired(false);

        builder.HasOne(x => x.MotherboardFactor)
               .WithMany()
               .HasForeignKey(x => x.MotherboardFactorId)
               .IsRequired(false);

        builder.HasOne(x => x.RAMType)
               .WithMany()
               .HasForeignKey(x => x.RAMTypeId)
               .IsRequired(false);

        builder.HasOne(x => x.RAMMode)
               .WithMany()
               .HasForeignKey(x => x.RAMModeId)
               .IsRequired(false);
    }
}