#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class ProcessorDtoViewModel
    {
        [Display(Prompt = nameof(TDP))]
        [Range(3, 300)]
        [Required]
        public int TDP { get; set; }

        [Display(Prompt = nameof(Title))]
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Display(Prompt = nameof(MaxRamSpeed))]
        [Range(1333, 5666)]
        [Required]
        public int MaxRamSpeed { get; set; }

        [Display(Prompt = nameof(Cores))]
        [Range(1, 256)]
        [Required]
        public int Cores { get; set; }

        [Display(Prompt = nameof(Threads))]
        [Range(1, 512)]
        [Required]
        public int Threads { get; set; }

        [Display(Prompt = nameof(Frequency))]
        [Range(1000, 7000)]
        [Required]
        public int Frequency { get; set; }

        [Display(Prompt = nameof(TurboBoostFrequency))]
        [Range(1000, 10000)]
        [Required]
        public int TurboBoostFrequency { get; set; }

        [Display(Prompt = nameof(TechProcess))]
        [Range(7, 48)]
        [Required]
        public int TechProcess { get; set; }

        [Display(Prompt = nameof(L1Cache))]
        [Required]
        public int L1Cache { get; set; }

        [Display(Prompt = nameof(L2Cache))]
        [Required]
        public int L2Cache { get; set; }

        [Display(Prompt = nameof(L3Cache))]
        [Required]
        public int L3Cache { get; set; }

        public bool IntegratedGraphics { get; set; }

        [Display(Prompt = nameof(Series))]
        [StringLength(15, MinimumLength = 3)]
        [Required]
        public string Series { get; set; }

        [Display(Prompt = nameof(ProcessorCoreId))]
        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProcessorCoreId { get; set; }

        [Display(Prompt = nameof(ProducerId))]
        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}