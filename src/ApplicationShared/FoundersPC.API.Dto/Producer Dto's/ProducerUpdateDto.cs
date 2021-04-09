﻿#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

#endregion

namespace FoundersPC.API.Dto
{
    public class ProducerUpdateDto
    {
        public string ShortName { get; set; }

        [StringLength(50, MinimumLength = 2)]
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