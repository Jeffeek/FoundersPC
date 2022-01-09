#region Using namespaces

using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace FoundersPC.Persistence.Configurations.Hardware;

public class CaseConfiguration : IEntityTypeConfiguration<Case>
{
    public void Configure(EntityTypeBuilder<Case> builder)
    {
        builder.HasOne(x => x.Metadata)
               .WithOne(x => x.Case)
               .HasForeignKey<CaseMetadata>(x => x.Id)
               .IsRequired();
    }
}