#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;
using FoundersPC.ApplicationShared.Validation;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware.HardDriveDisk
{
    public class HardDriveDiskInsertDtoViewModel
    {
        [ShouldBeIn(typeof(int), 5400, 7200)]
        [Required]
        public int HeadSpeed { get; set; }

        [Range(0, 1024)]
        [Required]
        public int BufferSize { get; set; }

        [Range(0, 100)]
        [Required]
        public int Noise { get; set; }

        [ShouldBeIn(typeof(double), 2.5, 3.5)]
        [Required]
        public double Factor { get; set; }

        [StringLength(20)]
        [Required]
        public string Interface { get; set; }

        [Range(60, 24576)]
        [Required]
        public int Volume { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int ProducerId { get; set; }
    }
}