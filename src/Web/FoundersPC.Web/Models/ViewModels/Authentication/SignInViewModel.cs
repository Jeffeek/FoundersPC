using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoundersPC.Web.Models.ViewModels.Authentication
{
    public class SignInViewModel
    {
		[Required]
        [Display(Name = "Email or Login",
                 Prompt = "Email or Login")]
        public string EmailOrLogin { get; set; }

		[Display(Name = "Password",
				 Prompt = "Password")]
		[StringLength(30, MinimumLength = 6)]
		[Required]
        public string RawPassword { get; set; }
    }
}
