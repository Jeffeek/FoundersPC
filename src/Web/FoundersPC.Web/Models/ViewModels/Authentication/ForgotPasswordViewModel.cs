using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoundersPC.Web.Models.ViewModels.Authentication
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "Email address",
                 Prompt = "Email address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
