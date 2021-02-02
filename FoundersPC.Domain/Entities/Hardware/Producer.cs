#region Using derectives

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.Domain.Entities.Hardware.Memory;
using FoundersPC.Domain.Entities.Hardware.Processor;
using FoundersPC.Domain.Entities.Hardware.VideoCard;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Domain.Entities.Hardware
{
	[Index(nameof(Id))]
	public class Producer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("Id")]
		[Required]
		public int Id { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[MinLength(2)]
		[MaxLength(20)]
		[DataType(DataType.Text)]
		[Column("ShortName")]
		public string ShortName { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[MinLength(3)]
		[MaxLength(100)]
		[DataType(DataType.Text)]
		[Column("FullName")]
		[Required]
		public string FullName { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[MaxLength(50)]
		[MinLength(3)]
		[DataType(DataType.Text)]
		[Column("Country")]
		public string Country { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[MaxLength(100)]
		[MinLength(5)]
		[DataType(DataType.Url)]
		[Column("Website")]
		public string Website { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataType(DataType.Date)]
		[Column("FoundationDate")]
		public DateTime? FoundationDate { get; set; }

		public ICollection<HDD> HardDrives { get; set; }
		public ICollection<SSD> SolidStateDrive { get; set; }
		public ICollection<RAM> RandomAccessMemory { get; set; }
		public ICollection<Case> Cases { get; set; }
		public ICollection<CPU> Processors { get; set; }
		public ICollection<GPU> VideoCards { get; set; }
		public ICollection<Motherboard> Motherboards { get; set; }
		public ICollection<PowerSupply> PowerSupplies { get; set; }
	}
}