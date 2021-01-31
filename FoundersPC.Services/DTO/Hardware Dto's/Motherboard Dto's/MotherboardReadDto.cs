using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersPC.Services.DTO
{
	public class MotherboardReadDto
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public ProducerReadDto Producer { get; set; }

		[DataType(DataType.Date)]
		public DateTime? MarketLaunch { get; set; }

		[MaxLength(10)]
		[MinLength(3)]
		[Required]
		public string Socket { get; set; }

		[Required]
		public double Factor { get; set; }

		[MinLength(5)]
		[MaxLength(6)]
		[Required]
		public string RAMSupport { get; set; }

		[Required]
		public int RAMSlots { get; set; }

		[MinLength(2)]
		[MaxLength(2)]
		[Required]
		public string RAMMode { get; set; }

		[Required]
		public bool SLIOrCrossfire { get; set; }

		[MinLength(3)]
		[MaxLength(20)]
		[Required]
		public string AudioSupport { get; set; }

		[Required]
		public bool WiFiSupport { get; set; }

		[Required]
		public bool PS2Support { get; set; }

		[Required]
		public int M2SlotsCount { get; set; }

		[DataType(DataType.Text)]
		[MinLength(3)]
		[MaxLength(12)]
		[Required]
		public string PCIExpressVersion { get; set; }
    }
}
