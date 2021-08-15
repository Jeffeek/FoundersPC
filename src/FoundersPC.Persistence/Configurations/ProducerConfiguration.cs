using FoundersPC.API.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations
{
    public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> builder)
        {
            builder.Property(x => x.ShortName)
                   .HasColumnType("nvarchar")
                   .HasColumnName("ShortName")
                   .IsRequired(false);

            builder.Property(x => x.FullName)
                   .HasColumnType("nvarchar")
                   .HasColumnName("FullName")
                   .IsRequired();

            builder.Property(x => x.CountryId)
                   .HasColumnType("int")
                   .HasColumnName("CountryId")
                   .IsRequired(false);

            builder.Property(x => x.Website)
                   .HasColumnType("nvarchar")
                   .HasColumnName("Website")
                   .HasMaxLength(1024)
                   .IsRequired(false);

            builder.Property(x => x.FoundationDate)
                   .HasColumnName("FoundationDate")
                   .HasColumnType("datetime")
                   .IsRequired(false);

            builder.HasOne(x => x.Country)
                   .WithMany()
                   .HasForeignKey(x => x.CountryId)
                   .IsRequired(false);

            builder.HasMany(x => x.SolidStateDrive)
                   .WithOne(x => x.Producer)
                   .HasForeignKey(x => x.ProducerId)
                   .IsRequired(false);

            builder.HasMany(x => x.HardDrives)
                   .WithOne(x => x.Producer)
                   .HasForeignKey(x => x.ProducerId)
                   .IsRequired(false);

            builder.HasMany(x => x.RandomAccessMemory)
                   .WithOne(x => x.Producer)
                   .HasForeignKey(x => x.ProducerId)
                   .IsRequired(false);

            builder.HasMany(x => x.Cases)
                   .WithOne(x => x.Producer)
                   .HasForeignKey(x => x.ProducerId)
                   .IsRequired(false);

            builder.HasMany(x => x.Processors)
                   .WithOne(x => x.Producer)
                   .HasForeignKey(x => x.ProducerId)
                   .IsRequired(false);

            builder.HasMany(x => x.VideoCards)
                   .WithOne(x => x.Producer)
                   .HasForeignKey(x => x.ProducerId)
                   .IsRequired(false);

            builder.HasMany(x => x.PowerSupplies)
                   .WithOne(x => x.Producer)
                   .HasForeignKey(x => x.ProducerId)
                   .IsRequired(false);

            builder.HasMany(x => x.Motherboards)
                   .WithOne(x => x.Producer)
                   .HasForeignKey(x => x.ProducerId)
                   .IsRequired(false);
        }
    }
}