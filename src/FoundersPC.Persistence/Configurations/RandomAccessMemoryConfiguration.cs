using FoundersPC.API.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations
{
    public class RandomAccessMemoryConfiguration : IEntityTypeConfiguration<RandomAccessMemory>
    {
        public void Configure(EntityTypeBuilder<RandomAccessMemory> builder)
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
        }
    }
}