using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersPC.Application
{
    public class RAMReadDto
    {
	    [Required]
	    public int Id { get; set; }

		[Required]
	    public ProducerReadDto Producer { get; set; }

	    [DataType(DataType.Date)]
	    public DateTime? MarketLaunch { get; set; }

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
