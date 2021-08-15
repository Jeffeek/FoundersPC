using FoundersPC.API.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations
{
    public class PowerSupplyConfiguration : IEntityTypeConfiguration<PowerSupply>
    {
        public void Configure(EntityTypeBuilder<PowerSupply> builder)
        {
            builder.Property(x => x.Power)
                   .HasColumnType("int")
                   .HasColumnName("PowerSupplyPower")
                   .IsRequired(false);

            builder.Property(x => x.Efficiency)
                   .HasColumnType("int")
                   .HasColumnName("PowerSupplyEfficiency")
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
}