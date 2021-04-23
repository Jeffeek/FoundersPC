#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class MotherboardDtoViewModel
    {
        [Display(Prompt = nameof(Socket))]
        [StringLength(10, MinimumLength = 3)]
        [Required]
        public string Socket { get; set; }

        [Display(Prompt = nameof(Factor))]
        [StringLength(10, MinimumLength = 3)]
        [Required]
        public string Factor { get; set; }

        [Display(Prompt = nameof(RAMSupport))]
        [StringLength(7, MinimumLength = 4)]
        [Required]
        public string RAMSupport { get; set; }

        [Display(Prompt = nameof(RAMSlots))]
        [Range(1, 6)]
        [Required]
        public int RAMSlots { get; set; }

        [Display(Prompt = nameof(RAMMode))]
        [StringLength(5, MinimumLength = 1)]
        [Required]
        public string RAMMode { get; set; }

        public bool SLIOrCrossfire { get; set; }

        [Display(Prompt = nameof(AudioSupport))]
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string AudioSupport { get; set; }

        public bool WiFiSupport { get; set; }

        public bool PS2Support { get; set; }

        [Display(Prompt = nameof(M2SlotsCount))]
        [Range(0, 6)]
        [Required]
        public int M2SlotsCount { get; set; }

        [Display(Prompt = nameof(PCIExpressVersion))]
        [StringLength(12, MinimumLength = 3)]
        [Required]
        public string PCIExpressVersion { get; set; }

        [Display(Prompt = nameof(Title))]
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Display(Prompt = nameof(ProducerId))]
        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}