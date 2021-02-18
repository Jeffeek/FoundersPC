#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.ApplicationShared.DTO.Users.ViewModels
{
    public class UserLoginViewModel
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email address is required!")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}