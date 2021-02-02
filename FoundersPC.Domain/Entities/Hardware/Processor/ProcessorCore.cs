using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoundersPC.Domain.Entities.Hardware.Processor
{
    public class ProcessorCore
    {
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    [Column("Id")]
	    [Key]
	    public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
	    [Column("MarketLaunch")]
	    [DataType(DataType.Date)]
	    public DateTime? MarketLaunch { get; set; }

	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [MinLength(3)]
        [Column("Title")]
	    [Required]
        public string Title { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        [MinLength(3)]
        [Column("MicroArchitecture")]
        [Required]
        public string MicroArchitecture { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("L2CachePerCore")]
        public int L2CachePerCore { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("L3CachePerCore")]
        public int L3CachePerCore { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        [MinLength(4)]
        [Column("Socket")]
        [Required]
        public string Socket { get; set; }

        public ICollection<CPU> Processors { get; set; }
    }
}
