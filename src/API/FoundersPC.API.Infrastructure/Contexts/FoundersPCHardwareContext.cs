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

        public DbSet<CPU> Processors { get; set; }

        public DbSet<ProcessorCore> ProcessorCores { get; set; }

        public DbSet<VideoCardCore> VideoCardCores { get; set; }

        public DbSet<GPU> VideoCards { get; set; }

        public DbSet<HDD> HardDrives { get; set; }

        public DbSet<SSD> SolidStateDrives { get; set; }

        public DbSet<Motherboard> Motherboards { get; set; }

        public DbSet<PowerSupply> PowerSupplies { get; set; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<RAM> RandomAccessMemory { get; set; }

        public DbSet<Case> Cases { get; set; }
    }
}