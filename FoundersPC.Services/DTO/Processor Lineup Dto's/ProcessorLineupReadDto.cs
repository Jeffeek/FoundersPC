using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoundersPC.Services.DTO
{
    public class ProcessorLineupReadDto
    {
	    [Required]
	    public int Id { get; set; }

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

	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
