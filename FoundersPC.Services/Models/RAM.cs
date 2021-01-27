using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
	[Index(nameof(Id))]
    public class RAM
    {
	    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MemoryType")]
        [MinLength(5)]
        [MaxLength(6)]
        public string MemoryType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ProducerId")]
        public int ProducerId { get; set; }

        [ForeignKey(nameof(ProducerId))]
        public Producer Producer { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Volume")]
        public int Volume { get; set; }

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
        [Column("AMP")]
        public bool AMP { get; set; }
    }
}
