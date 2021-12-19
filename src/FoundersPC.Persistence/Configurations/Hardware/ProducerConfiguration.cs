#region Using namespaces

using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace FoundersPC.Persistence.Configurations.Hardware;

public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
{
    public void Configure(EntityTypeBuilder<Producer> builder)
    {
        builder.ToTable("Producers");
        builder.HasKey(x => x.Id);

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

        builder.HasMany(x => x.SolidStateDriveMetadata)
               .WithOne(x => x.Producer)
               .HasForeignKey(x => x.ProducerId)
               .IsRequired(false);

        builder.HasMany(x => x.HardDrivesMetadata)
               .WithOne(x => x.Producer)
               .HasForeignKey(x => x.ProducerId)
               .IsRequired(false);

        builder.HasMany(x => x.RandomAccessMemoryMetadata)
               .WithOne(x => x.Producer)
               .HasForeignKey(x => x.ProducerId)
               .IsRequired(false);

        builder.HasMany(x => x.CasesMetadata)
               .WithOne(x => x.Producer)
               .HasForeignKey(x => x.ProducerId)
               .IsRequired(false);

        builder.HasMany(x => x.ProcessorsMetadata)
               .WithOne(x => x.Producer)
               .HasForeignKey(x => x.ProducerId)
               .IsRequired(false);

        builder.HasMany(x => x.VideoCardsMetadata)
               .WithOne(x => x.Producer)
               .HasForeignKey(x => x.ProducerId)
               .IsRequired(false);

        builder.HasMany(x => x.PowerSuppliesMetadata)
               .WithOne(x => x.Producer)
               .HasForeignKey(x => x.ProducerId)
               .IsRequired(false);

        builder.HasMany(x => x.MotherboardsMetadata)
               .WithOne(x => x.Producer)
               .HasForeignKey(x => x.ProducerId)
               .IsRequired(false);
    }
}