using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoundersPC.ApplicationShared.AccountSettings.Request
{
    public class UserChangePasswordRequest
    {
        [Required]
        public int UserId { get; set; }

        [StringLength(maximumLength : 30,
                      MinimumLength = 6,
                      ErrorMessage = "Min 6, max 30")]
        [PasswordPropertyText]
        [Required]
        public string OldPassword { get; set; }

        [StringLength(maximumLength : 30,
                      MinimumLength = 6,
                      ErrorMessage = "Min 6, max 30")]
        [PasswordPropertyText]
        [Required]
        public string NewPassword { get; set; }
    }
}
