using FoundersPC.API.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations
{
    public class SolidStateDriveConfiguration : IEntityTypeConfiguration<SolidStateDrive>
    {
        public void Configure(EntityTypeBuilder<SolidStateDrive> builder)
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

            builder.Property(x => x.MicroScheme)
                   .HasColumnType("nvarchar")
                   .HasColumnName("MicroScheme")
                   .HasMaxLength(2048)
                   .IsRequired(false);

            builder.Property(x => x.SequentialRead)
                   .HasColumnType("numeric")
                   .HasColumnName("SequentialRead")
                   .IsRequired(false);

            builder.Property(x => x.SequentialRecording)
                   .HasColumnType("numeric")
                   .HasColumnName("SequentialRecording")
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