using FoundersPC.API.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations
{
    public class VideoCardConfiguration : IEntityTypeConfiguration<VideoCard>
    {
        public void Configure(EntityTypeBuilder<VideoCard> builder)
        {
            builder.Property(x => x.TDP)
                   .HasColumnType("int")
                   .HasColumnName("TDP")
                   .IsRequired(false);

            builder.Property(x => x.AdditionalPower)
                   .HasColumnType("int")
                   .HasColumnName("AdditionalPower")
                   .IsRequired(false);

            builder.Property(x => x.TDP)
                   .HasColumnType("int")
                   .HasColumnName("TDP")
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
                   .HasColumnName("MemoryVolume")
                   .IsRequired(false);

            builder.Property(x => x.VideoMemoryId)
                   .HasColumnType("int")
                   .HasColumnName("VideoMemoryId")
                   .IsRequired(false);

            builder.Property(x => x.MemoryFrequency)
                   .HasColumnType("int")
                   .HasColumnName("MemoryFrequency")
                   .IsRequired(false);

            builder.Property(x => x.MemoryBusWidth)
                   .HasColumnType("int")
                   .HasColumnName("MemoryBusWidth")
                   .IsRequired(false);

            builder.HasOne(x => x.VideoMemory)
                   .WithMany()
                   .HasForeignKey(x => x.VideoMemoryId)
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
        }
    }
}