using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersPC.Services.DTO
{
    public class ProcessorLineupUpdateDto
    {
	    [DataType(DataType.Date)]
	    public DateTime? MarketLaunch { get; set; }

	    [DataType(DataType.Text)]
	    [MaxLength(20)]
	    [MinLength(3)]
	    [Required]
	    public string Serial { get; set; }

	    [DataType(DataType.Text)]
	    [MaxLength(50)]
	    [MinLength(3)]
	    [Required]
	    public string FamilyCodename { get; set; }

	    [DataType(DataType.Text)]
	    [MaxLength(30)]
	    [MinLength(3)]
	    [Required]
	    public string MicroArchitecture { get; set; }

	    [Required]
	    public int TechProcess { get; set; }

	    [DataType(DataType.Text)]
	    [MaxLength(15)]
	    [MinLength(3)]
	    [Required]
	    public string Socket { get; set; }
	}
}
