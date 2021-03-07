using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoundersPC.Web.Models.ViewModels
{
    public class UserPasswordViewModel
    {
        [StringLength(maximumLength : 30,
                      MinimumLength = 6,
                      ErrorMessage = "Min 6, max 30")]
        [PasswordPropertyText]
        [Required]
        public string OldPassword { get; set; }

        [StringLength(maximumLength : 30,
                      MinimumLength = 6,
                      ErrorMessage = "Min 6, max 30")]
        [Compare(nameof(NewPassword),
                 ErrorMessage = "New password and New password confirmation should be equal")]
        [PasswordPropertyText]
        [Required]
        public string NewPassword { get; set; }

        [StringLength(maximumLength : 30,
                      MinimumLength = 6,
                      ErrorMessage = "Min 6, max 30")]
        [Compare(nameof(NewPassword),
                 ErrorMessage = "New password and New password confirmation should be equal")]
        [PasswordPropertyText]
        [Required]
        public string NewPasswordConfirm { get; set; }
    }
}
