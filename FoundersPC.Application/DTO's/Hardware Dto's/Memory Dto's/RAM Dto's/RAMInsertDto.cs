#region Using derectives

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Application
{
	public class RAMInsertDto
	{
		[Required]
		public int ProducerId { get; set; }

		[Required]
		public string Title { get; set; }

		[MinLength(5)]
		[MaxLength(15)]
		[Required]
		public string MemoryType { get; set; }

		[Required]
		public int Frequency { get; set; }

		[MinLength(3)]
		[MaxLength(5)]
		[Required]
		public string CASLatency { get; set; }

		[MinLength(5)]
		[MaxLength(8)]
		[Required]
		public string Timings { get; set; }

		[Required]
		public double Voltage { get; set; }

		[Required]
		public bool XMP { get; set; }

		[Required]
		public bool ECC { get; set; }

		[Required]
		public int PCIndex { get; set; }
	}
}