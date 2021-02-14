#region Using derectives

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Application
{
	public class SSDUpdateDto
	{
		[Required]
		public int ProducerId { get; set; }

		[DataType(DataType.Date)]
		public DateTime? MarketLaunch { get; set; }

		[Required]
		public double Factor { get; set; }

		[MinLength(3)]
		[MaxLength(20)]
		[Required]
		public string Interface { get; set; }

		[Required]
		public int Volume { get; set; }

		[MinLength(3)]
		[MaxLength(50)]
		[Required]
		public string MicroScheme { get; set; }

		[Required]
		public int SequentialRead { get; set; }

		[Required]
		public int SequentialRecording { get; set; }
	}
}