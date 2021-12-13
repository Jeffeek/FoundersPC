#region Using namespaces

using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace FoundersPC.Persistence.Configurations.Hardware;

public class ProcessorConfiguration : IEntityTypeConfiguration<Processor>
{
    public void Configure(EntityTypeBuilder<Processor> builder)
    {
        builder.HasOne(x => x.Metadata)
               .WithOne(x => x.Processor)
               .HasForeignKey<Processor>(x => x.Id)
               .IsRequired();
    }
}