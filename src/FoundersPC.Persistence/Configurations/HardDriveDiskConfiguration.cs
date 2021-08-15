using FoundersPC.API.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations
{
    public class HardDriveDiskConfiguration : IEntityTypeConfiguration<HardDriveDisk>
    {
        public void Configure(EntityTypeBuilder<HardDriveDisk> builder)
        {
            builder.Property(x => x.DiskFactorId)
                   .HasColumnName("DiskFactorId")
                   .HasColumnType("int")
                   .IsRequired(false);

            builder.Property(x => x.DiskConnectionInterfaceId)
                   .HasColumnType("int")
                   .HasColumnName("DiskConnectionInterfaceId")
                   .IsRequired(false);

            builder.Property(x => x.Volume)
                   .HasColumnType("int")
                   .HasColumnName("Volume")
                   .IsRequired(false);

            builder.Property(x => x.BufferSize)
                   .HasColumnType("int")
                   .HasColumnName("BufferSize")
                   .IsRequired(false);

            builder.Property(x => x.HeadSpeed)
                   .HasColumnType("int")
                   .HasColumnName("HeadSpeed")
                   .IsRequired(false);

            builder.Property(x => x.Noise)
                   .HasColumnType("numeric")
                   .HasColumnName("Noise")
                   .IsRequired(false);

            builder.HasOne(x => x.Factor)
                   .WithMany()
                   .HasForeignKey(x => x.DiskFactorId)
                   .IsRequired(false);

            builder.HasOne(x => x.Interface)
                   .WithMany()
                   .HasForeignKey(x => x.DiskConnectionInterfaceId)
                   .IsRequired(false);
        }
    }
}