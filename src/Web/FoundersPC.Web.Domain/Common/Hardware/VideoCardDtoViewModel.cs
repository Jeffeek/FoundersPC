#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class VideoCardDtoViewModel
    {
        [Display(Prompt = nameof(AdditionalPower))]
        [Range(0, Int32.MaxValue)]
        [Required]
        public int AdditionalPower { get; set; }

        [Display(Prompt = nameof(Frequency))]
        [Range(1, Int32.MaxValue)]
        [Required]
        public int Frequency { get; set; }

        [Display(Prompt = nameof(Series))]
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Series { get; set; }

        [Display(Prompt = nameof(VideoMemoryType))]
        [Range(1, Int32.MaxValue)]
        [Required]
        public int VideoMemoryVolume { get; set; }

        [Display(Prompt = nameof(VideoMemoryType))]
        [StringLength(8, MinimumLength = 4)]
        [Required]
        public string VideoMemoryType { get; set; }

        [Display(Prompt = nameof(VideoMemoryFrequency))]
        [Range(1, Int32.MaxValue)]
        [Required]
        public int VideoMemoryFrequency { get; set; }

        [Display(Prompt = nameof(VideoMemoryBusWidth))]
        [Range(1, Int32.MaxValue)]
        [Required]
        public int VideoMemoryBusWidth { get; set; }

        [Display(Prompt = nameof(VGA))]
        [Range(0, 6)]
        [Required]
        public int VGA { get; set; }

        [Display(Prompt = nameof(DVI))]
        [Range(0, 6)]
        [Required]
        public int DVI { get; set; }

        [Display(Prompt = nameof(HDMI))]
        [Range(0, 6)]
        [Required]
        public int HDMI { get; set; }

        [Display(Prompt = nameof(DisplayPort))]
        [Range(0, 6)]
        [Required]
        public int DisplayPort { get; set; }

        [Display(Prompt = nameof(Title))]
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Display(Prompt = nameof(GraphicsProcessorId))]
        [Range(1, Int32.MaxValue)]
        [Required]
        public int GraphicsProcessorId { get; set; }

        [Display(Prompt = nameof(ProducerId))]
        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}