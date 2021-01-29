﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersPC.Services.DTO
{
    public class SSDReadDto
    {
	    [Required]
	    public int Id { get; set; }

	    [Required]
		public ProducerReadDto Producer { get; set; }

	    [DataType(DataType.Date)]
	    public DateTime? MarketLaunch { get; set; }

	    [Required]
	    public double Factor { get; set; }

	    [MinLength(3)]
	    [MaxLength(20)]
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
