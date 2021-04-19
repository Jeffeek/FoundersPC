#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware.ProcessorCore
{
    public class ProcessorCoreUpdateDtoViewModel
    {
        public DateTime MarketLaunch { get; set; }

        public bool IsMarketLaunchEmpty { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string MicroArchitecture { get; set; }

        [Range(0, 512_000)]
        [Required]
        public int L2Cache { get; set; }

        [Range(0, 512_000)]
        [Required]
        public int L3Cache { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 4)]
        public string Socket { get; set; }
    }
}