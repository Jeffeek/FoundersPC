#region Using namespaces

using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace FoundersPC.Persistence.Configurations.Hardware;

public class VideoCardConfiguration : IEntityTypeConfiguration<VideoCard>
{
    public void Configure(EntityTypeBuilder<VideoCard> builder)
    {
        builder.HasOne(x => x.Metadata)
               .WithOne(x => x.VideoCard)
               .HasForeignKey<VideoCard>(x => x.Id)
               .IsRequired();
    }
}