using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
	[Index(nameof(Id))]
    public class CPU : EquipmentEntityBase
    {
	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TechProcess")]
        public int TechProcess { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        [MinLength(3)]
        [Column("Lineup")]
        public string Lineup { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TDP")]
        public int TDP { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MaxRamSpeed")]
        public int MaxRamSpeed { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        [MinLength(3)]
        [Column("Socket")]
        public string Socket { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Cores")]
        public int Cores { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Frequency")]
        public int Frequency { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TurboBoostFrequency")]
        public int TurboBoostFrequency { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("L3Cache")]
        public int L3Cache { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IntegratedGraphics")]
        public bool IntegratedGraphics { get; set; }
    }
}
