using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Domain.Enums;
using FoundersPC.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoundersPC.Persistence.Configurations
{
    public class HardwareBaseConfiguration : IEntityTypeConfiguration<HardwareBase>
    {
        public void Configure(EntityTypeBuilder<HardwareBase> builder)
        {
            builder.ToTable("Hardware");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProducerId)
                   .HasColumnType("int")
                   .HasColumnName("ProducerId")
                   .IsRequired();

            builder.Property(x => x.Title)
                   .HasColumnType("nvarchar")
                   .HasColumnName("Title")
                   .HasMaxLength(512)
                   .IsRequired();

            builder.HasOne(x => x.Producer)
                   .WithMany()
                   .HasForeignKey(x => x.ProducerId)
                   .IsRequired();

            builder.HasDiscriminator(x => x.Type)
                   .HasValue<Case>(HardwareType.Case)
                   .HasValue<Processor>(HardwareType.CPU)
                   .HasValue<VideoCard>(HardwareType.GPU)
                   .HasValue<RandomAccessMemory>(HardwareType.RAM)
                   .HasValue<Motherboard>(HardwareType.Motherboard)
                   .HasValue<PowerSupply>(HardwareType.PowerSupply)
                   .HasValue<HardDriveDisk>(HardwareType.HardDriveDisk)
                   .HasValue<SolidStateDrive>(HardwareType.SolidStateDrive)
                   .IsComplete();

            builder.AddAuditableColumns();
        }
    }
}