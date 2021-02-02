#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Domain.Entities.Hardware.Processor
{
	[Index(nameof(Id))]
	public class CPU : EquipmentEntityBase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("TDP")]
		[Required]
		public int TDP { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("ProcessorCoreId")]
		[Required]
		public int ProcessorCoreId { get; set; }

		[ForeignKey(nameof(ProcessorCoreId))]
		public ProcessorCore Core { get; set; }

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
		[Column("Threads")]
		[Required]
		public int Threads { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Frequency")]
		[Required]
		public int Frequency { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("TurboBoostFrequency")]
		[Required]
		public int TurboBoostFrequency { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("TechProcess")]
		[Required]
		public int TechProcess { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("L1Cache")]
		[Required]
		public int L1Cache { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("L2Cache")]
		[Required]
		public int L2Cache { get; set; }

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