#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.AuthenticationShared.Request
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "Email can't be empty")]
        [Display(Name = "Email", Prompt = "example@email.com")]
        [EmailAddress(ErrorMessage = "Incorrect email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be empty (min 6, max 30)")]
        [Display(Name = "Password")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Min 6, max 30")]
        public string Password { get; set; }
    }
}