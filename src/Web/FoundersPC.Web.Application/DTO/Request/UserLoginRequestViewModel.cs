using System.ComponentModel.DataAnnotations;

namespace FoundersPC.Web.Application.DTO.Request
{
    public class UserLoginRequestViewModel
    {
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Ok. Goodbye. Piece of shit")]
        [Required(ErrorMessage = "Login or email can't be empty")]
        [Display(Name = "Email or Login", Prompt = "Email OR Login(if you have it)")]
        public string LoginOrEmail { get; set; }

        [Required(ErrorMessage = "Password can't be empty (min 6, max 30)")]
        [Display(Name = "Password", Prompt = "Password: 6 chars min, 30 max")]
        [StringLength(maximumLength : 30, MinimumLength = 6, ErrorMessage = "Min 6, max 30")]
        public string Password { get; set; }
    }
}
