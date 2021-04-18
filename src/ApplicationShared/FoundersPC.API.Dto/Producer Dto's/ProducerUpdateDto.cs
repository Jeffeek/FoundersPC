#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.API.Dto
{
    public class ProducerUpdateDto
    {
        [StringLength(50)]
        public string ShortName { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Required]
        public string FullName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Country { get; set; }

        [StringLength(100)]
        [Url]
        public string Website { get; set; }

        public DateTime? FoundationDate { get; set; }
    }
}