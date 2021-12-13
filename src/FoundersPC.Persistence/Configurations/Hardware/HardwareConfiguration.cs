#region Using namespaces

using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HardwareType = FoundersPC.Domain.Enums.HardwareType;

#endregion

namespace FoundersPC.Persistence.Configurations.Hardware;

public class HardwareConfiguration : IEntityTypeConfiguration<Domain.Entities.Hardware.Hardware>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Hardware.Hardware> builder)
    {
        builder.ToTable("Hardware");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.HardwareType)
               .WithMany()
               .HasForeignKey(x => x.HardwareTypeId)
               .IsRequired();

        builder.HasDiscriminator<int>("HardwareTypeId")
               .HasValue<Case>((int)HardwareType.Case)
               .HasValue<Processor>((int)HardwareType.CPU)
               .HasValue<VideoCard>((int)HardwareType.GPU)
               .HasValue<RandomAccessMemory>((int)HardwareType.RAM)
               .HasValue<Motherboard>((int)HardwareType.Motherboard)
               .HasValue<PowerSupply>((int)HardwareType.PowerSupply)
               .HasValue<HardDriveDisk>((int)HardwareType.HardDriveDisk)
               .HasValue<SolidStateDrive>((int)HardwareType.SolidStateDrive)
               .IsComplete();

        builder.AddAuditableColumns();
    }
}