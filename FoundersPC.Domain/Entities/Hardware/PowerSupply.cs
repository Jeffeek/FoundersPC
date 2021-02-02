#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Domain.Entities.Hardware
{
	[Index(nameof(Id))]
	public class PowerSupply : EquipmentEntityBase
	{
		[Column("Power")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int Power { get; set; }

		[Column("Efficiency")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Efficiency { get; set; }

		[MinLength(1)]
		[MaxLength(10)]
		[Column("MotherboardPowering")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public string MotherboardPowering { get; set; }

		[Column("IsModular")]
		[Required]
		public bool IsModular { get; set; }

		[Column("CPU4PIN")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public bool CPU4PIN { get; set; }

		[Column("CPU8PIN")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public bool CPU8PIN { get; set; }

		[Column("FanDiameter")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int FanDiameter { get; set; }

		[Column("Certificate80PLUS")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public bool Certificate80PLUS { get; set; }

		[Column("PFC")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public bool PFC { get; set; }
	}
}