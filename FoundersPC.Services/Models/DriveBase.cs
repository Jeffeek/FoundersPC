using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
	[Index(nameof(Id))]
    public abstract class DriveBase
    {
	    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ProducerId")]
        public int ProducerId { get; set; }

        [ForeignKey(nameof(ProducerId))]
        public Producer Producer { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Volume")]
        public int Volume { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Factor")]
        public double Factor { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Interface")]
        [MinLength(3)]
        [MaxLength(10)]
        public string Interface { get; set; }
    }
}
