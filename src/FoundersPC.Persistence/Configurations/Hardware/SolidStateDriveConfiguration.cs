#region Using namespaces

using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace FoundersPC.Persistence.Configurations.Hardware;

public class SolidStateDriveConfiguration : IEntityTypeConfiguration<SolidStateDrive>
{
    public void Configure(EntityTypeBuilder<SolidStateDrive> builder)
    {
        builder.HasOne(x => x.Metadata)
               .WithOne(x => x.SolidStateDrive)
               .HasForeignKey<SolidStateDrive>(x => x.Id)
               .IsRequired();
    }
}