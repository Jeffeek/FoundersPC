#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class SolidStateDriveDtoViewModel
    {
        [Required]
        public double Factor { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Interface { get; set; }

        [Range(30, 10_000)]
        [Required]
        public int Volume { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string MicroScheme { get; set; }

        [Range(0, Int32.MaxValue)]
        [Required]
        public int SequentialRead { get; set; }

        [Range(0, Int32.MaxValue)]
        [Required]
        public int SequentialRecording { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}