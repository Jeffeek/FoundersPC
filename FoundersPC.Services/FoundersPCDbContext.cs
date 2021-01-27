using System;
using System.Collections.Generic;
using System.Text;
using FoundersPC.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services
{
    public class FoundersPCDbContext : DbContext
    {
	    public FoundersPCDbContext(DbContextOptions<FoundersPCDbContext> options) : base(options)
	    {
		    Database.EnsureCreated();
	    }


	    public DbSet<ChipProducer> ChipProducers { get; set; }
		public DbSet<CPU> CPUs { get; set; }
		public DbSet<CrystalSerial> CrystalSerials { get; set; }
		public DbSet<GPU> GPUs { get; set; }
		public DbSet<HDD> HardDrives { get; set; }
		public DbSet<Motherboard> Motherboards { get; set; }
		public DbSet<PowerSupply> PowerSupplies { get; set; }
		public DbSet<Producer> Producers { get; set; }
		public DbSet<RAM> RAMs { get; set; }
		public DbSet<SSD> SolidStateDrives { get; set; }
    }
}
