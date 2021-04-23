#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class ProcessorCoreDtoViewModel
    {
        public DateTime MarketLaunch { get; set; }

        public bool IsMarketLaunchEmpty { get; set; }

        [Display(Prompt = nameof(Title))]
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Prompt = nameof(MicroArchitecture))]
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string MicroArchitecture { get; set; }

        [Display(Prompt = nameof(L2Cache))]
        [Range(0, 512_000)]
        [Required]
        public int L2Cache { get; set; }

        [Display(Prompt = nameof(L3Cache))]
        [Range(0, 512_000)]
        [Required]
        public int L3Cache { get; set; }

        [Display(Prompt = nameof(Socket))]
        [Required]
        [StringLength(10, MinimumLength = 4)]
        public string Socket { get; set; }
    }
}