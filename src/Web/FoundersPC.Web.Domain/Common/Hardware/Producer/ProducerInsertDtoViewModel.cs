#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Hardware.Producer
{
    public class ProducerInsertDtoViewModel
    {
        public string ShortName { get; set; }

        public bool IsShortNameEmpty { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Required]
        public string FullName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Country { get; set; }

        [StringLength(100)]
        [Url]
        public string Website { get; set; }

        public bool IsWebsiteEmpty { get; set; }

        public DateTime FoundationDate { get; set; }

        public bool IsFoundationDateEmpty { get; set; }
    }
}