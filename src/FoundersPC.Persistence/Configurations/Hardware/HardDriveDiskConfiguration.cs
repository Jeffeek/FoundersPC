#region Using namespaces

using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace FoundersPC.Persistence.Configurations.Hardware;

public class HardDriveDiskConfiguration : IEntityTypeConfiguration<HardDriveDisk>
{
    public void Configure(EntityTypeBuilder<HardDriveDisk> builder)
    {
        builder.HasOne(x => x.Metadata)
               .WithOne(x => x.HardDriveDisk)
               .HasForeignKey<HardDriveDisk>(x => x.Id)
               .IsRequired();
    }
}