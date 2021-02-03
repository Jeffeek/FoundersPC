#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Domain.Entities.Hardware.VideoCard
{
	[Index(nameof(Id))]
	public class GPU : EquipmentEntityBase
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("GraphicsProcessorId")]
		[MinLength(3)]
		[MaxLength(20)]
		[DataType(DataType.Text)]
		[Required]
		public int GraphicsProcessorId { get; set; }

		[ForeignKey(nameof(GraphicsProcessorId))]
		public VideoCardCore Core { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("AdditionalPower")]
		[Required]
		public int AdditionalPower { get; set; }

		#region Memory

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("VideoMemoryVolume")]
		[Required]
		public int VideoMemoryVolume { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("VideoMemoryType")]
		[MinLength(4)]
		[MaxLength(7)]
		[Required]
		public string VideoMemoryType { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("VideoMemoryFrequency")]
		[MinLength(3)]
		[MaxLength(5)]
		[Required]
		public int VideoMemoryFrequency { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("VideoMemoryBusWidth")]
		[MinLength(2)]
		[MaxLength(4)]
		[Required]
		public int VideoMemoryBusWidth { get; set; }

		#endregion

		#region Output

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("VGA")]
		[Required]
		public int VGA { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("DVI")]
		[Required]
		public int DVI { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("HDMI")]
		[Required]
		public int HDMI { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("DisplayPort")]
		[Required]
		public int DisplayPort { get; set; }

		#endregion
	}
}