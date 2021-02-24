#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.AuthenticationShared.Request
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Login or email can't be empty")]
        [Display(Name = "Email or Login", Prompt = "example@email.com")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Login or email can't be so short")]
        public string LoginOrEmail { get; set; }

        [Required(ErrorMessage = "Password can't be empty (min 6, max 30)")]
        [Display(Name = "Password")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Min 6, max 30")]
        public string Password { get; set; }
    }
}