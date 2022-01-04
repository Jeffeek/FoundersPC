#region Using namespaces

using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace FoundersPC.Persistence.Configurations.Hardware;

public class MotherboardConfiguration : IEntityTypeConfiguration<Motherboard>
{
    public void Configure(EntityTypeBuilder<Motherboard> builder)
    {
        builder.HasOne(x => x.Metadata)
               .WithOne(x => x.Motherboard)
               .HasForeignKey<Motherboard>(x => x.Id)
               .IsRequired();
    }
}