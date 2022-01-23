using FoundersPC.Domain.Entities.Hardware.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Hardware.Metadata;

public class VideoCardMetadataConfiguration : IEntityTypeConfiguration<VideoCardMetadata>
{
    public void Configure(EntityTypeBuilder<VideoCardMetadata> builder)
    {
        builder.Property(x => x.TDP)
               .HasColumnType("int")
               .HasColumnName("TDP")
               .IsRequired(false);

        builder.Property(x => x.AdditionalPower)
               .HasColumnType("int")
               .HasColumnName("AdditionalPower")
               .IsRequired(false);

        builder.Property(x => x.Frequency)
               .HasColumnType("int")
               .HasColumnName("Frequency")
               .IsRequired(false);

        builder.Property(x => x.Series)
               .HasColumnType("nvarchar")
               .HasColumnName("Series")
               .IsRequired(false);

        builder.Property(x => x.MemoryVolume)
               .HasColumnType("int")
               .HasColumnName("Volume")
               .IsRequired(false);

        builder.Property(x => x.VideoMemoryTypeId)
               .HasColumnType("int")
               .HasColumnName("VideoMemoryTypeId")
               .IsRequired(false);

        builder.Property(x => x.MemoryFrequency)
               .HasColumnType("int")
               .HasColumnName("MemoryFrequency")
               .IsRequired(false);

        builder.Property(x => x.MemoryBusWidth)
               .HasColumnType("int")
               .HasColumnName("MemoryBusWidth")
               .IsRequired(false);

        builder.Property(x => x.IsIntegrated)
               .HasColumnType("bit")
               .HasColumnName("IsIntegrated")
               .IsRequired(false);

        builder.HasOne(x => x.VideoMemoryType)
               .WithMany()
               .HasForeignKey(x => x.VideoMemoryTypeId)
               .IsRequired(false);

        builder.Property(x => x.VGA)
               .HasColumnType("int")
               .HasColumnName("VGA")
               .IsRequired(false);

        builder.Property(x => x.DVI)
               .HasColumnType("int")
               .HasColumnName("DVI")
               .IsRequired(false);

        builder.Property(x => x.HDMI)
               .HasColumnType("int")
               .HasColumnName("HDMI")
               .IsRequired(false);

        builder.Property(x => x.DisplayPort)
               .HasColumnType("int")
               .HasColumnName("DisplayPort")
               .IsRequired(false);

        builder.HasOne(x => x.VideoCard)
               .WithOne(x => x.Metadata)
               .HasForeignKey<VideoCardMetadata>(x => x.Id)
               .IsRequired();
    }
}