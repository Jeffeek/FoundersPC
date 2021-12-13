#region Using namespaces

using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace FoundersPC.Persistence.Configurations.Hardware;

public class RandomAccessMemoryConfiguration : IEntityTypeConfiguration<RandomAccessMemory>
{
    public void Configure(EntityTypeBuilder<RandomAccessMemory> builder)
    {
        builder.HasOne(x => x.Metadata)
               .WithOne(x => x.RandomAccessMemory)
               .HasForeignKey<RandomAccessMemory>(x => x.Id)
               .IsRequired();
    }
}