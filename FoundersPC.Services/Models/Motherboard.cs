using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
	[Index(nameof(Id))]
    public class Motherboard : EquipmentEntityBase
    {
	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        [MinLength(3)]
        [Column("Socket")]
        public string Socket { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Factor")]
        public double Factor { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RAMSupport")]
        [MinLength(5)]
        [MaxLength(6)]
        public string RAMSupport { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RAMSlots")]
        public int RAMSlots { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RAMMode")]
        [MinLength(2)]
        [MaxLength(2)]
        public string RAMMode { get; set; }

        [DatabaseGenerated((DatabaseGeneratedOption.None))]
        [Column("SLI_Crossfire")]
        public bool SLIOrCrossfire { get; set; }

        [DatabaseGenerated((DatabaseGeneratedOption.None))]
        [Column("AudioSupport")]
        [MinLength(3)]
        [MaxLength(20)]
        public string AudioSupport { get; set; }

        [DatabaseGenerated((DatabaseGeneratedOption.None))]
        [Column("WiFiSupport")]
        public bool WiFiSupport { get; set; }

        [DatabaseGenerated((DatabaseGeneratedOption.None))]
        [Column("PS2Support")]
        public bool PS2Support { get; set; }

        [DatabaseGenerated((DatabaseGeneratedOption.None))]
        [Column("M2SlotsCount")]
        public int M2SlotsCount { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("PCIExpressVersion")]
        [DataType(DataType.Text)]
        [MinLength(3)]
        [MaxLength(12)]
        public string PCIExpressVersion { get; set; }
    }
}
