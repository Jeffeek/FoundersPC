using System.ComponentModel.DataAnnotations;

namespace FoundersPC.AuthenticationShared.Request
{
    public class UserChangePasswordRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(30)]
        [MinLength(6)]
        [Required]
        public string OldPassword { get; set; }

        [MaxLength(30)]
        [MinLength(6)]
        [Required]
        public string NewPassword { get; set; }
    }
}
