using FoundersPC.Domain.Entities.Hardware.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Hardware.Metadata;

public class PowerSupplyMetadataConfiguration : IEntityTypeConfiguration<PowerSupplyMetadata>
{
    public void Configure(EntityTypeBuilder<PowerSupplyMetadata> builder)
    {
        builder.Property(x => x.Power)
               .HasColumnType("int")
               .HasColumnName("Power")
               .IsRequired(false);

        builder.Property(x => x.Efficiency)
               .HasColumnType("int")
               .HasColumnName("Efficiency")
               .IsRequired(false);

        builder.Property(x => x.MotherboardPoweringId)
               .HasColumnType("int")
               .HasColumnName("MotherboardPoweringId")
               .IsRequired(false);

        builder.Property(x => x.IsModular)
               .HasColumnType("bit")
               .HasColumnName("IsModular")
               .IsRequired(false);

        builder.Property(x => x.CPU4PIN)
               .HasColumnType("bit")
               .HasColumnName("CPU4PIN")
               .IsRequired(false);

        builder.Property(x => x.CPU8PIN)
               .HasColumnType("bit")
               .HasColumnName("CPU8PIN")
               .IsRequired(false);

        builder.Property(x => x.FanDiameter)
               .HasColumnType("int")
               .HasColumnName("FanDiameter")
               .IsRequired(false);

        builder.Property(x => x.Certificate80PLUS)
               .HasColumnType("bit")
               .HasColumnName("Certificate80PLUS")
               .IsRequired(false);

        builder.Property(x => x.PFC)
               .HasColumnType("bit")
               .HasColumnName("PFC")
               .IsRequired(false);

        builder.HasOne(x => x.MotherboardPowering)
               .WithMany()
               .HasForeignKey(x => x.MotherboardPoweringId)
               .IsRequired(false);
    }
}