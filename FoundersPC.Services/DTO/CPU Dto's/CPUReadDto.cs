using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersPC.Services.DTO
{
    public class CPUReadDto
    {
	    [Required]
	    public int Id { get; set; }

	    public ProducerReadDto Producer { get; set; }

	    [DataType(DataType.Date)]
	    public DateTime? MarketLaunch { get; set; }

	    [Required]
	    public int TDP { get; set; }

	    public ProcessorLineupReadDto ProcessorLineup { get; set; }

	    [DataType(DataType.Text)]
	    [MaxLength(20)]
	    [MinLength(3)]
	    [Required]
	    public string Name { get; set; }

	    [Required]
	    public int MaxRamSpeed { get; set; }

	    [Required]
	    public int Cores { get; set; }

	    [Required]
	    public int Frequency { get; set; }

	    [Required]
	    public int TurboBoostFrequency { get; set; }

	    [Required]
	    public int L3Cache { get; set; }

	    [Required]
	    public bool IntegratedGraphics { get; set; }
	}
}
