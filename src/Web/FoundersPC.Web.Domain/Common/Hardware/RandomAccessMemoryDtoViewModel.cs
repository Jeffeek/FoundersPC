#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class RandomAccessMemoryDtoViewModel
    {
        [StringLength(15, MinimumLength = 3)]
        [Required]
        public string MemoryType { get; set; }

        [Range(0, 8666)]
        [Required]
        public int Frequency { get; set; }

        [StringLength(5, MinimumLength = 3)]
        [Required]
        public string CASLatency { get; set; }

        [RegularExpression("^\\d{1,2}(-\\d{1,2}-\\d{1,2}(-\\d{1,2})?)?$")]
        [StringLength(11, MinimumLength = 1)]
        [Required]
        public string Timings { get; set; }

        [Range(1d, 2d)]
        [Required]
        public double Voltage { get; set; }

        public bool XMP { get; set; }

        public bool ECC { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int PCIndex { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}