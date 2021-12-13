using FoundersPC.Domain.Entities.Hardware.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Hardware.Metadata;

public class ProcessorMetadataConfiguration : IEntityTypeConfiguration<ProcessorMetadata>
{
    public void Configure(EntityTypeBuilder<ProcessorMetadata> builder)
    {
        builder.Property(x => x.TDP)
               .HasColumnType("int")
               .HasColumnName("TDP")
               .IsRequired(false);

        builder.Property(x => x.MaxRamSpeed)
               .HasColumnType("int")
               .HasColumnName("MaxRamSpeed")
               .IsRequired(false);

        builder.Property(x => x.CoresCount)
               .HasColumnType("int")
               .HasColumnName("CoresCount")
               .IsRequired(false);

        builder.Property(x => x.ThreadsCount)
               .HasColumnType("int")
               .HasColumnName("ThreadsCount")
               .IsRequired(false);

        builder.Property(x => x.Frequency)
               .HasColumnType("int")
               .HasColumnName("Frequency")
               .IsRequired(false);

        builder.Property(x => x.TurboBoostFrequency)
               .HasColumnType("int")
               .HasColumnName("TurboBoostFrequency")
               .IsRequired(false);

        builder.Property(x => x.TechProcess)
               .HasColumnType("int")
               .HasColumnName("TechProcess")
               .IsRequired(false);

        builder.Property(x => x.L1Cache)
               .HasColumnType("int")
               .HasColumnName("L1Cache")
               .IsRequired(false);

        builder.Property(x => x.L2Cache)
               .HasColumnType("int")
               .HasColumnName("L2Cache")
               .IsRequired(false);

        builder.Property(x => x.L3Cache)
               .HasColumnType("int")
               .HasColumnName("L3Cache")
               .IsRequired(false);

        builder.Property(x => x.Series)
               .HasColumnType("nvarchar")
               .HasColumnName("Series")
               .IsRequired(false);

        builder.Property(x => x.IntegratedGraphicsId)
               .HasColumnType("int")
               .HasColumnName("IntegratedGraphicsId")
               .IsRequired(false);

        builder.HasOne(x => x.IntegratedGraphics)
               .WithMany()
               .HasForeignKey(x => x.IntegratedGraphicsId)
               .IsRequired(false);

        builder.HasOne(x => x.Processor)
               .WithOne(x => x.Metadata)
               .HasForeignKey<ProcessorMetadata>(x => x.Id)
               .IsRequired();
    }
}