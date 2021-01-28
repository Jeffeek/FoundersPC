#region Using derectives

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Models
{
	[Index(nameof(Id))]
	public class GPU : EquipmentEntityBase
	{
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

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Interface")]
		[MinLength(5)]
		[MaxLength(30)]
		[DataType(DataType.Text)]
		[Required]
		public string Interface { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("GraphicsProcessor")]
		[MinLength(3)]
		[MaxLength(20)]
		[DataType(DataType.Text)]
		[Required]
		public string GraphicsProcessor { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Frequency")]
		[MinLength(3)]
		[MaxLength(5)]
		[Required]
		public int Frequency { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("DirectX")]
		[MinLength(1)]
		[MaxLength(3)]
		[Required]
		public int DirectX { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("SLI_Crossfire")]
		[Required]
		public bool SLIOrCrossfire { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("AdditionalPower")]
		[Required]
		public int AdditionalPower { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("Width")]
		[Required]
		public int Width { get; set; }

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
	}
}