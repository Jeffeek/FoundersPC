#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Models.Hardware
{
	[Index(nameof(Id))]
	public class CPU : EquipmentEntityBase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("TDP")]
		[Required]
		public int TDP { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ProcessorLineupId")]
		[Required]
		public int ProcessorLineupId { get; set; }

		[ForeignKey(nameof(ProcessorLineupId))]
		public ProcessorLineup ProcessorLineup { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataType(DataType.Text)]
		[MaxLength(20)]
		[MinLength(3)]
		[Column("Name")]
		[Required]
		public string Name { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("MaxRamSpeed")]
		[Required]
		public int MaxRamSpeed { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Cores")]
		[Required]
		public int Cores { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Frequency")]
		[Required]
		public int Frequency { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("TurboBoostFrequency")]
		[Required]
		public int TurboBoostFrequency { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("L3Cache")]
		[Required]
		public int L3Cache { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("IntegratedGraphics")]
		[Required]
		public bool IntegratedGraphics { get; set; }
	}
}