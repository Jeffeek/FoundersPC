#region Using namespaces

using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Domain.Entities.Hardware.Metadata;
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

        builder.AddIdentity();

        builder.HasOne(x => x.HardwareType)
               .WithMany()
               .HasForeignKey(x => x.HardwareTypeId)
               .IsRequired();

        builder.HasOne(x => x.BaseMetadata)
               .WithOne()
               .HasForeignKey<HardwareMetadata>(x => x.Id)
               .HasPrincipalKey<Domain.Entities.Hardware.Hardware>(x => x.Id)
               .IsRequired();

        builder.HasDiscriminator<int>("HardwareTypeId")
               .HasValue<Case>((int)HardwareType.Case)
               .HasValue<Processor>((int)HardwareType.CPU)
               .HasValue<VideoCard>((int)HardwareType.GPU)
               .HasValue<RandomAccessMemory>((int)HardwareType.RAM)
               .HasValue<Motherboard>((int)HardwareType.MB)
               .HasValue<PowerSupply>((int)HardwareType.PowerSupply)
               .HasValue<HardDriveDisk>((int)HardwareType.HDD)
               .HasValue<SolidStateDrive>((int)HardwareType.SSD)
               .IsComplete();

        builder.AddFullAuditableColumns();
    }
}