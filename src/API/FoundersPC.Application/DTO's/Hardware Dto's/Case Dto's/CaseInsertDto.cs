#region Using derectives

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Application
{
	public class CaseInsertDto
	{
		[Required]
		public int ProducerId { get; set; }

		[MaxLength(100)]
		[MinLength(0)]
		[Required]
		public string Title { get; set; }

		[MinLength(3)]
		[MaxLength(40)]
		[Required]
		public string Type { get; set; }

		[MinLength(3)]
		[MaxLength(20)]
		[Required]
		public string MaxMotherboardSize { get; set; }

		[MinLength(3)]
		[MaxLength(50)]
		[Required]
		public string Material { get; set; }

		[MinLength(3)]
		[MaxLength(50)]
		[Required]
		public string WindowMaterial { get; set; }

		[Required]
		public bool TransparentWindow { get; set; }

		[MinLength(2)]
		[MaxLength(50)]
		[Required]
		public string Color { get; set; }

		[Range(0.1, 100)]
		public double? Weight { get; set; }

		[Range(0.1, 1000)]
		public int? Height { get; set; }

		[Range(0.1, 1000)]
		public int? Width { get; set; }

		[Range(0.1, 1000)]
		public int? Depth { get; set; }
	}
}