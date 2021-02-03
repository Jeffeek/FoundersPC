#region Using derectives

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Application
{
	public class HDDInsertDto
	{
		[Required]
		public int ProducerId { get; set; }

		[DataType(DataType.Date)]
		public DateTime? MarketLaunch { get; set; }

		[Required]
		public int HeadSpeed { get; set; }

		[Required]
		public int BufferSize { get; set; }

		public int Noise { get; set; }

		[Required]
		public double Factor { get; set; }

		[MinLength(3)]
		[MaxLength(20)]
		public string Interface { get; set; }

		[Required]
		public int Volume { get; set; }
	}
}