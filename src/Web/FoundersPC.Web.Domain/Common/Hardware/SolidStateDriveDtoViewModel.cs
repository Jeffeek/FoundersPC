#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class SolidStateDriveDtoViewModel
    {
        [Display(Prompt = nameof(Factor))]
        [Required]
        public double Factor { get; set; }

        [Display(Prompt = nameof(Interface))]
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Interface { get; set; }

        [Display(Prompt = nameof(Volume))]
        [Range(30, 10_000)]
        [Required]
        public int Volume { get; set; }

        [Display(Prompt = nameof(MicroScheme))]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string MicroScheme { get; set; }

        [Display(Prompt = nameof(SequentialRead))]
        [Range(0, Int32.MaxValue)]
        [Required]
        public int SequentialRead { get; set; }

        [Display(Prompt = nameof(SequentialRecording))]
        [Range(0, Int32.MaxValue)]
        [Required]
        public int SequentialRecording { get; set; }

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