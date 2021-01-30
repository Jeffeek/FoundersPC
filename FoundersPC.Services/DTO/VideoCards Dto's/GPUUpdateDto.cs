using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoundersPC.Services.DTO
{
    public class GPUUpdateDto
    {
		[Required]
		public int ProducerId { get; set; }

		[DataType(DataType.Date)]
		public DateTime? MarketLaunch { get; set; }

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

		[MinLength(3)]
		[MaxLength(20)]
		[DataType(DataType.Text)]
		[Required]
		public string GraphicsProcessor { get; set; }

		[MinLength(3)]
		[MaxLength(5)]
		[Required]
		public int Frequency { get; set; }

		[MinLength(1)]
		[MaxLength(3)]
		[Required]
		public int DirectX { get; set; }

		[Required]
		public bool SLIOrCrossfire { get; set; }

		[Required]
		public int AdditionalPower { get; set; }

		[Required]
		public int Width { get; set; }

		[Required]
		public int VGA { get; set; }

		[Required]
		public int DVI { get; set; }

		[Required]
		public int HDMI { get; set; }

		[Required]
		public int DisplayPort { get; set; }
	}
}
