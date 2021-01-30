using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.Services.Models.Hardware;

namespace FoundersPC.Services.DTO
{
    public class CaseReadDto
    {
	    [Required]
		public int Id { get; set; }

		[Required]
		public int ProducerId { get; set; }

		[ForeignKey(nameof(ProducerId))]
		public ProducerReadDto Producer { get; set; }

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
