using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoundersPC.Web.Models.ViewModels
{
    public class UserSecurityViewModel
    {
        [Display(Name = "New login", Prompt = "New login")]
        [StringLength(maximumLength : 30,
                      MinimumLength = 5,
                      ErrorMessage = "Min 5, max 30")]
        [Required]
        public string NewLogin { get; set; }
    }
}
