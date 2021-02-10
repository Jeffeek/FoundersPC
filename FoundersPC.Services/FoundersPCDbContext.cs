#region Using derectives

using FoundersPC.Services.Models.Hardware;
using FoundersPC.Services.Models.Hardware.Memory;
using FoundersPC.Services.Models.Hardware.Processor;
using FoundersPC.Services.Models.Hardware.VideoCard;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services
{
	public sealed class FoundersPCDbContext : DbContext
	{
		public FoundersPCDbContext(DbContextOptions<FoundersPCDbContext> options) : base(options)
		{
			
		}

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