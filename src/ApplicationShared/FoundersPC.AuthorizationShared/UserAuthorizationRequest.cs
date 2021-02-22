using System.ComponentModel.DataAnnotations;

namespace FoundersPC.AuthorizationShared
{
    public class UserAuthorizationRequest
    {
        [Required(ErrorMessage = "Email or login has incorrect value", AllowEmptyStrings = false)]
        public string LoginOrEmail { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string RawPassword { get; set; }
    }
}
