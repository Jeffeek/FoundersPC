using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.Web.Domain.Common.Hardware.Producers
{
    public class ProducerUpdateDtoViewModel
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

        public DateTime FoundationDate { get; set; }

        public bool IsFoundationDateEmpty { get; set; }
    }
}
