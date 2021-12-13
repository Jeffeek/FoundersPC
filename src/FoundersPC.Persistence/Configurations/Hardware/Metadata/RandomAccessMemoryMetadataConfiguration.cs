using FoundersPC.Domain.Entities.Hardware.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Hardware.Metadata;

internal class RandomAccessMemoryMetadataConfiguration : IEntityTypeConfiguration<RandomAccessMemoryMetadata>
{
    public void Configure(EntityTypeBuilder<RandomAccessMemoryMetadata> builder)
    {
        builder.Property(x => x.RAMTypeId)
               .HasColumnType("int")
               .HasColumnName("RAMTypeId")
               .IsRequired(false);

        builder.Property(x => x.Frequency)
               .HasColumnType("int")
               .HasColumnName("Frequency")
               .IsRequired(false);

        builder.Property(x => x.Timings)
               .HasColumnType("int")
               .HasColumnName("Timings")
               .IsRequired(false);

        builder.Property(x => x.Voltage)
               .HasColumnType("numeric")
               .HasColumnName("Voltage")
               .IsRequired(false);

        builder.Property(x => x.XMP)
               .HasColumnType("bit")
               .HasColumnName("XMP")
               .IsRequired(false);

        builder.Property(x => x.ECC)
               .HasColumnType("bit")
               .HasColumnName("ECC")
               .IsRequired(false);

        builder.Property(x => x.PCIndex)
               .HasColumnType("int")
               .HasColumnName("PCIndex")
               .IsRequired(false);

        builder.Property(x => x.Volume)
               .HasColumnType("int")
               .HasColumnName("Volume")
               .IsRequired(false);

        builder.HasOne(x => x.RAMType)
               .WithMany()
               .HasForeignKey(x => x.RAMTypeId)
               .IsRequired(false);

        builder.HasOne(x => x.RandomAccessMemory)
               .WithOne(x => x.Metadata)
               .HasForeignKey<RandomAccessMemoryMetadata>(x => x.Id)
               .IsRequired();
    }
}