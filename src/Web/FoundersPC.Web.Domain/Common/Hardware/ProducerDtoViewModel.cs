#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware
{
    public class ProducerDtoViewModel
    {
        [Display(Prompt = nameof(ShortName))]
        public string ShortName { get; set; }

        public bool IsShortNameEmpty { get; set; }

        [Display(Prompt = nameof(FullName))]
        [StringLength(100, MinimumLength = 2)]
        [Required]
        public string FullName { get; set; }

        [Display(Prompt = nameof(Country))]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Country { get; set; }

        [Display(Prompt = nameof(Website))]
        [StringLength(100)]
        [Url]
        public string Website { get; set; }

        public bool IsWebsiteEmpty { get; set; }

        public DateTime FoundationDate { get; set; }

        public bool IsFoundationDateEmpty { get; set; }
    }
}