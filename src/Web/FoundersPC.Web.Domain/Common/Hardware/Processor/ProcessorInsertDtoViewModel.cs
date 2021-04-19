#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware.Processor
{
    public class ProcessorInsertDtoViewModel
    {
        [Range(3, 300)]
        [Required]
        public int TDP { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Range(1333, 5666)]
        [Required]
        public int MaxRamSpeed { get; set; }

        [Range(1, 256)]
        [Required]
        public int Cores { get; set; }

        [Range(1, 512)]
        [Required]
        public int Threads { get; set; }

        [Range(1000, 7000)]
        [Required]
        public int Frequency { get; set; }

        [Range(1000, 10000)]
        [Required]
        public int TurboBoostFrequency { get; set; }

        [Range(7, 48)]
        [Required]
        public int TechProcess { get; set; }

        [Required]
        public int L1Cache { get; set; }

        [Required]
        public int L2Cache { get; set; }

        [Required]
        public int L3Cache { get; set; }

        public bool IntegratedGraphics { get; set; }

        [MaxLength(15)]
        [StringLength(15, MinimumLength = 3)]
        [Required]
        public string Series { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProcessorCoreId { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}