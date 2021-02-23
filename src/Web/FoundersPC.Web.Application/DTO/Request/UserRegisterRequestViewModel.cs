using System.ComponentModel.DataAnnotations;

namespace FoundersPC.Web.Application.DTO.Request
{
    public class UserRegisterRequestViewModel
    {
        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "Incorrect email address")]
        [Display(Name = "Email", Prompt = "example@email.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be empty (min 6, max 30)")]
        [Display(Name = "Password", Prompt = "Password: 6 chars min, 30 max")]
        [StringLength(maximumLength : 30, MinimumLength = 6, ErrorMessage = "Min 6, max 30")]
        public string Password { get; set; }
    }
}
