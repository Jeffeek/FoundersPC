#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware.PowerSupply
{
    public class PowerSupplyUpdateDtoViewModel
    {
        [Required]
        [Range(50, 10000)]
        public int Power { get; set; }

        [Range(50, 100)]
        public int Efficiency { get; set; }

        public bool IsEfficiencyEmpty { get; set; }

        [RegularExpression("2[0,4](\\+[4,8])?")]
        [StringLength(10, MinimumLength = 1)]
        public string MotherboardPowering { get; set; }

        public bool IsModular { get; set; }

        public bool CPU4PIN { get; set; }

        public bool IsCPU4PINEmpty { get; set; }

        public bool CPU8PIN { get; set; }

        public bool IsCPU8PINEmpty { get; set; }

        [Range(0, 300)]
        public int FanDiameter { get; set; }

        public bool Certificate80PLUS { get; set; }

        public bool PFC { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Range(1, Int32.MaxValue)]
        public int ProducerId { get; set; }
    }
}