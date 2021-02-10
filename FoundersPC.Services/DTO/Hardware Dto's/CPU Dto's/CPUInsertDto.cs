using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersPC.Services.DTO
{
    public class CPUInsertDto
    {
	    [Required]
	    public int ProducerId { get; set; }

	    [DataType(DataType.Date)]
	    public DateTime? MarketLaunch { get; set; }

	    [Required]
	    public int TDP { get; set; }

	    [Required]
	    public int ProcessorCoreId { get; set; }

	    [DataType(DataType.Text)]
	    [MaxLength(20)]
	    [MinLength(3)]
	    [Required]
	    public string Name { get; set; }

	    [Required]
	    public int MaxRamSpeed { get; set; }

	    [Required]
	    public int TechProcess { get; set; }

		[Required]
	    public int Cores { get; set; }

	    [Required]
	    public int Threads { get; set; }

	    [Required]
	    public int Frequency { get; set; }

	    [Required]
	    public int TurboBoostFrequency { get; set; }

		[Required]
	    public int L1Cache { get; set; }

		[Required]
	    public int L2Cache { get; set; }

		[Required]
	    public int L3Cache { get; set; }

	    [Required]
	    public bool IntegratedGraphics { get; set; }
	}
}
