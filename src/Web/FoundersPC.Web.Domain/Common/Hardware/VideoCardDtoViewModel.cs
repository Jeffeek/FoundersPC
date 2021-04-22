#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class VideoCardDtoViewModel
    {
        [Range(0, Int32.MaxValue)]
        [Required]
        public int AdditionalPower { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int Frequency { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Series { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int VideoMemoryVolume { get; set; }

        [StringLength(8, MinimumLength = 4)]
        [Required]
        public string VideoMemoryType { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int VideoMemoryFrequency { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int VideoMemoryBusWidth { get; set; }

        [Range(0, 6)]
        [Required]
        public int VGA { get; set; }

        [Range(0, 6)]
        [Required]
        public int DVI { get; set; }

        [Range(0, 6)]
        [Required]
        public int HDMI { get; set; }

        [Range(0, 6)]
        [Required]
        public int DisplayPort { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int GraphicsProcessorId { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}