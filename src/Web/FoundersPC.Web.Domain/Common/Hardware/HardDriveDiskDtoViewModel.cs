#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;
using FoundersPC.ApplicationShared.Validation;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class HardDriveDiskDtoViewModel
    {
        [Display(Prompt = nameof(HeadSpeed))]
        [Range(2000, 10000)]
        [Required]
        public int HeadSpeed { get; set; }

        [Display(Prompt = nameof(BufferSize))]
        [Range(0, 1024)]
        [Required]
        public int BufferSize { get; set; }

        [Display(Prompt = nameof(Noise))]
        [Range(0, 100)]
        [Required]
        public int Noise { get; set; }

        [Display(Prompt = nameof(Factor))]
        [Required]
        public double Factor { get; set; }

        [Display(Prompt = nameof(Interface))]
        [StringLength(20)]
        [Required]
        public string Interface { get; set; }

        [Display(Prompt = nameof(Volume))]
        [Range(60, 24576)]
        [Required]
        public int Volume { get; set; }

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