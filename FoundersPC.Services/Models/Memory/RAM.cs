using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models.Memory
{
	[Index(nameof(Id))]
    public class RAM : EquipmentEntityBase
    {
	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MemoryType")]
        [MinLength(5)]
        [MaxLength(15)]
        public string MemoryType { get; set; }

	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Frequency")]
        public int Frequency { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CASLatency")]
        [MinLength(3)]
        [MaxLength(5)]
        public string CASLatency { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Timings")]
        [MinLength(5)]
        [MaxLength(8)]
        public string Timings { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Voltage")]
        public double Voltage { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("XMP")]
        public bool XMP { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ECC")]
        public bool ECC { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("PCIndex")]
        public int PCIndex { get; set; }
    }
}
