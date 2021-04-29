#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class RandomAccessMemoryDtoViewModel
    {
        [Display(Prompt = nameof(MemoryType))]
        [StringLength(15, MinimumLength = 3)]
        [Required]
        public string MemoryType { get; set; }

        [Display(Prompt = nameof(Frequency))]
        [Range(0, 8666)]
        [Required]
        public int Frequency { get; set; }

        [Display(Prompt = nameof(CASLatency))]
        [StringLength(5, MinimumLength = 3)]
        [Required]
        public string CASLatency { get; set; }

        [Display(Prompt = nameof(Timings))]
        [RegularExpression("^\\d{1,2}(-\\d{1,2}-\\d{1,2}(-\\d{1,2})?)?$")]
        [StringLength(11, MinimumLength = 1)]
        [Required]
        public string Timings { get; set; }

        [Display(Prompt = nameof(Voltage))]
        [Range(1d, 2d)]
        [Required]
        public double Voltage { get; set; }

        public bool XMP { get; set; }

        public bool ECC { get; set; }

        [Display(Prompt = nameof(PCIndex))]
        [Range(1, Int32.MaxValue)]
        [Required]
        public int PCIndex { get; set; }

        [Display(Prompt = nameof(Title))]
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Range(1, 512)]
        [Required]
        public int Volume { get; set; }

        [Display(Prompt = nameof(ProducerId))]
        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}