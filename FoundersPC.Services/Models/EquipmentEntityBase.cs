using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoundersPC.Services.Models
{
    public abstract class EquipmentEntityBase
    {
	    [Key]
	    [Column("Id")]
	    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public int Id { get; set; }

	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
	    [Column("ProducerId")]
	    public int ProducerId { get; set; }

	    [ForeignKey(nameof(ProducerId))]
	    public Producer Producer { get; set; }

	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
	    [Column("MarketLaunch")]
	    [DataType(DataType.Date)]
	    public DateTime? MarketLaunch { get; set; }
	}
}
