using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoundersPC.Services.Models
{
    public class SSD : DriveBase
    {
	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MicroScheme")]
        [MinLength(3)]
        [MaxLength(50)]
        public string MicroScheme { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ChipProducerId")]
        public int ChipProducerId { get; set; }

        [ForeignKey(nameof(ChipProducerId))]
        public ChipProducer ChipsProducer { get; set; }
    }
}
