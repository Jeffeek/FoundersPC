using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.Web.Domain.Entities.ViewModels.Entrances
{
    public class EntrancesBetweenFilter
    {
        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime Finish { get; set; }
    }
}
