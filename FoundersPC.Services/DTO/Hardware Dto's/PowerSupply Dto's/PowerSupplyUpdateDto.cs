using System;
using System.ComponentModel.DataAnnotations;

namespace FoundersPC.Services.DTO
{
    public class PowerSupplyUpdateDto
    {
	    [Required]
        public int ProducerId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? MarketLaunch { get; set; }

        [Required]
        public int Power { get; set; }

        public int Efficiency { get; set; }

        [MinLength(1)]
        [MaxLength(10)]
        [Required]
        public string MotherboardPowering { get; set; }

        [Required]
        public bool IsModular { get; set; }

        [Required]
        public bool CPU4PIN { get; set; }

        [Required]
        public bool CPU8PIN { get; set; }

        [Required]
        public int FanDiameter { get; set; }

        [Required]
        public bool Certificate80PLUS { get; set; }

        [Required]
        public bool PFC { get; set; }
	}
}
