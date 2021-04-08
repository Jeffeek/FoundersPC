#region Using namespaces

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.AccountSettings
{
    public class PasswordSettingsViewModel
    {
        [StringLength(30,
                      MinimumLength = 6,
                      ErrorMessage = "Min 6, max 30")]
        [PasswordPropertyText]
        [Required]
        public string OldPassword { get; set; }

        [StringLength(30,
                      MinimumLength = 6,
                      ErrorMessage = "Min 6, max 30")]
        [Compare(nameof(NewPassword),
                 ErrorMessage = "New password and New password confirmation should be equal")]
        [PasswordPropertyText]
        [Required]
        public string NewPassword { get; set; }

        [StringLength(30,
                      MinimumLength = 6,
                      ErrorMessage = "Min 6, max 30")]
        [Compare(nameof(NewPassword),
                 ErrorMessage = "New password and New password confirmation should be equal")]
        [PasswordPropertyText]
        [Required]
        public string NewPasswordConfirm { get; set; }
    }
}