#region Using namespaces

using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Domain.Entities.Memory;
using FoundersPC.API.Domain.Entities.Processor;
using FoundersPC.API.Domain.Entities.VideoCard;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Contexts
{
    public class FoundersPCHardwareContext : DbContext
    {
        public FoundersPCHardwareContext(DbContextOptions<FoundersPCHardwareContext> options) : base(options) { }

        public DbSet<ProcessorEntity> Processors { get; set; }

        public DbSet<ProcessorCore> ProcessorCores { get; set; }

        public DbSet<VideoCardCoreEntity> VideoCardCores { get; set; }

        public DbSet<VideoCardEntity> VideoCards { get; set; }

        public DbSet<HardDriveDiskEntity> HardDrives { get; set; }

        public DbSet<SolidStateDriveEntity> SolidStateDrives { get; set; }

        public DbSet<MotherboardEntity> Motherboards { get; set; }

        public DbSet<PowerSupplyEntity> PowerSupplies { get; set; }

        public DbSet<ProducerEntity> Producers { get; set; }

        public DbSet<RandomAccessMemoryEntity> RandomAccessMemory { get; set; }

        public DbSet<CaseEntity> Cases { get; set; }
    }
}