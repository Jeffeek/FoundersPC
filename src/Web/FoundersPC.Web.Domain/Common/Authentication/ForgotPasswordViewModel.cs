#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Common.Authentication
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