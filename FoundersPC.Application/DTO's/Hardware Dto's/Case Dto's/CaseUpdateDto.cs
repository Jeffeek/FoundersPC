using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersPC.Application
{
    public class CaseUpdateDto
    {
	    [Required]
		public int ProducerId { get; set; }

	    [DataType(DataType.Date)]
		public DateTime? MarketLaunch { get; set; }

		[MinLength(40)]
		[MaxLength(3)]
		[DataType(DataType.Text)]
		[Required]
		public string Type { get; set; }

		[MinLength(3)]
		[MaxLength(20)]
		[DataType(DataType.Text)]
		[Required]
		public string MaxMotherboardSize { get; set; }

		[MinLength(3)]
		[MaxLength(50)]
		[DataType(DataType.Text)]
		[Required]
		public string Material { get; set; }

		[MinLength(3)]
		[MaxLength(50)]
		[DataType(DataType.Text)]
		[Required]
		public string WindowMaterial { get; set; }

		[Required]
		public bool TransparentWindow { get; set; }

		[MinLength(2)]
		[MaxLength(50)]
		[DataType(DataType.Text)]
		[Required]
		public string Color { get; set; }
	}
}
