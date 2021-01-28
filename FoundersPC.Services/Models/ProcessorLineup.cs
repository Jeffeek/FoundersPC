using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoundersPC.Services.Models
{
    public class ProcessorLineup
    {
	    [Key]
	    [Column("Id")]
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    [Required]
	    public int Id { get; set; }

	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
	    [Column("MarketLaunch")]
	    [DataType(DataType.Date)]
	    public DateTime? MarketLaunch { get; set; }

	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        [MinLength(3)]
        [Column("Serial")]
        [Required]
        public string Serial { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [MinLength(3)]
        [Column("FamilyCodename")]
        [Required]
        public string FamilyCodename { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        [MinLength(3)]
        [Column("MicroArchitecture")]
        [Required]
        public string MicroArchitecture { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TechProcess")]
        [Required]
        public int TechProcess { get; set; }
    }
}
