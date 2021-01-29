#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Models.Hardware.Memory
{
	[Index(nameof(Id))]
	public class RAM : EquipmentEntityBase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("MemoryType")]
		[MinLength(5)]
		[MaxLength(15)]
		[Required]
		public string MemoryType { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Frequency")]
		[Required]
		public int Frequency { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("CASLatency")]
		[MinLength(3)]
		[MaxLength(5)]
		[Required]
		public string CASLatency { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Timings")]
		[MinLength(5)]
		[MaxLength(8)]
		[Required]
		public string Timings { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Voltage")]
		[Required]
		public double Voltage { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("XMP")]
		[Required]
		public bool XMP { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ECC")]
		[Required]
		public bool ECC { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("PCIndex")]
		[Required]
		public int PCIndex { get; set; }
	}
}