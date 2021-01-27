using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoundersPC.Services.Models
{
    public class PowerSupply
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MinLength(1)]
        [MaxLength(10)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Power")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Power { get; set; }

        [Column("ProducerId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProducerId { get; set; }

        [ForeignKey(nameof(ProducerId))]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Producer Producer { get; set; }

        [MinLength(1)]
        [MaxLength(10)]
        [Column("MotherboardPowering")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MotherboardPowering { get; set; }

        [Column("MotherboardPowering")]
        public bool IsModular { get; set; }

        [Column("MotherboardPowering")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool CPU4PIN { get; set; }

        [Column("MotherboardPowering")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool CPU8PIN { get; set; }

        [MinLength(1)]
        [MaxLength(10)]
        [Column("FanDiameter")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FanDiameter { get; set; }

        [Column("Sertificate80PLUS")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool Sertificate80PLUS { get; set; }

        [Column("PFC")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool PFC { get; set; }
    }
}
