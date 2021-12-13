#region Using namespaces

using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace FoundersPC.Persistence.Configurations.Hardware;

public class PowerSupplyConfiguration : IEntityTypeConfiguration<PowerSupply>
{
    public void Configure(EntityTypeBuilder<PowerSupply> builder)
    {
        builder.HasOne(x => x.Metadata)
               .WithOne(x => x.PowerSupply)
               .HasForeignKey<PowerSupply>(x => x.Id)
               .IsRequired();
    }
}