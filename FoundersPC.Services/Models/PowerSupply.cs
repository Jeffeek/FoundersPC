﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Models
{
	[Index(nameof(Id))]
    public class PowerSupply
    {
	    [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Power")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Power { get; set; }

        [Column("ProducerId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProducerId { get; set; }

        [ForeignKey(nameof(ProducerId))]
        public Producer Producer { get; set; }

        [MinLength(1)]
        [MaxLength(10)]
        [Column("MotherboardPowering")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MotherboardPowering { get; set; }

        [Column("IsModular")]
        public bool IsModular { get; set; }

        [Column("CPU4PIN")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool CPU4PIN { get; set; }

        [Column("CPU8PIN")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool CPU8PIN { get; set; }

        [Column("FanDiameter")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FanDiameter { get; set; }

        [Column("Certificate80PLUS")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool Certificate80PLUS { get; set; }

        [Column("PFC")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool PFC { get; set; }
    }
}
