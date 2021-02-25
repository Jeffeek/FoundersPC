#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.AuthenticationShared.Request
{
    public class UserForgotPasswordRequest
    {
        [EmailAddress(ErrorMessage = "Incorrect email address")]
        [Display(Name = "Email", Prompt = "example@email.com")]
        [Required(ErrorMessage = "Email can't be empty")]
        public string Email { get; set; }
    }
}