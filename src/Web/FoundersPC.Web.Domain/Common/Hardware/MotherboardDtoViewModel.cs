#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class MotherboardDtoViewModel
    {
        [StringLength(10, MinimumLength = 3)]
        [Required]
        public string Socket { get; set; }

        [StringLength(10, MinimumLength = 3)]
        [Required]
        public string Factor { get; set; }

        [StringLength(7, MinimumLength = 4)]
        [Required]
        public string RAMSupport { get; set; }

        [Range(1, 6)]
        [Required]
        public int RAMSlots { get; set; }

        [StringLength(5, MinimumLength = 1)]
        [Required]
        public string RAMMode { get; set; }

        public bool SLIOrCrossfire { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string AudioSupport { get; set; }

        public bool WiFiSupport { get; set; }

        public bool PS2Support { get; set; }

        [Range(0, 6)]
        [Required]
        public int M2SlotsCount { get; set; }

        [StringLength(12, MinimumLength = 3)]
        [Required]
        public string PCIExpressVersion { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}