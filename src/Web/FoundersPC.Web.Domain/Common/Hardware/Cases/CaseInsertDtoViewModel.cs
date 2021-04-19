using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.Web.Domain.Common.Hardware.Cases
{
	public class CaseInsertDtoViewModel
	{
		[StringLength(40, MinimumLength = 3)]
		[Required]
		public string Type { get; set; }

		[StringLength(50, MinimumLength = 3)]
		[Required]
		public string WindowMaterial { get; set; }

		[StringLength(20, MinimumLength = 3)]
		[Required]
		public string MaxMotherboardSize { get; set; }

		[StringLength(50, MinimumLength = 3)]
		[Required]
		public string Material { get; set; }

		public bool TransparentWindow { get; set; }

		[StringLength(50, MinimumLength = 3)]
		[Required]
		public string Color { get; set; }

		[Range(1, Int32.MaxValue)]
		public int Depth { get; set; }

		public bool IsDepthEmpty { get; set; }

		[Range(1, Int32.MaxValue)]
		public int Height { get; set; }

		public bool IsHeightEmpty { get; set; }

		[StringLength(100, MinimumLength = 1)]
		[Required]
		public string Title { get; set; }

		[Range(1, Double.MaxValue)]
		public double Weight { get; set; }

		public bool IsWeightEmpty { get; set; }

		[Range(1, Int32.MaxValue)]
		public int Width { get; set; }

		public bool IsWidthEmpty { get; set; }

		[Range(1, Int64.MaxValue)]
		[Required]
		public int ProducerId { get; set; }
	}
}
