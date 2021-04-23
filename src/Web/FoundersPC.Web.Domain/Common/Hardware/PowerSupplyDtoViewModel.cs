#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class PowerSupplyDtoViewModel
    {
        [Display(Prompt = nameof(Power))]
        [Required]
        [Range(50, 10000)]
        public int Power { get; set; }

        [Display(Prompt = nameof(Efficiency))]
        [Range(50, 100)]
        public int Efficiency { get; set; }

        public bool IsEfficiencyEmpty { get; set; }

        [Display(Prompt = nameof(MotherboardPowering))]
        [RegularExpression("2[0,4](\\+[4,8])?")]
        [StringLength(10, MinimumLength = 1)]
        public string MotherboardPowering { get; set; }

        public bool IsModular { get; set; }

        public bool CPU4PIN { get; set; }

        public bool IsCPU4PINEmpty { get; set; }

        public bool CPU8PIN { get; set; }

        public bool IsCPU8PINEmpty { get; set; }

        [Display(Prompt = nameof(FanDiameter))]
        [Range(0, 300)]
        [Required]
        public int FanDiameter { get; set; }

        public bool Certificate80PLUS { get; set; }

        public bool PFC { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Prompt = nameof(Title))]
        public string Title { get; set; }

        [Required]
        [Display(Prompt = nameof(ProducerId))]
        [Range(1, Int32.MaxValue)]
        public int ProducerId { get; set; }
    }
}