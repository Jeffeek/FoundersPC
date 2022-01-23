using FoundersPC.Domain.Entities.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations.Metadata;

public class SocketConfiguration : IEntityTypeConfiguration<Socket>
{
    public void Configure(EntityTypeBuilder<Socket> builder)
    {
        builder.HasMany(x => x.Motherboards)
               .WithOne(x => x.Socket)
               .HasForeignKey(x => x.SocketId)
               .IsRequired(false);

        builder.HasMany(x => x.Processors)
               .WithOne(x => x.Socket)
               .HasForeignKey(x => x.SocketId)
               .IsRequired(false);
    }
}