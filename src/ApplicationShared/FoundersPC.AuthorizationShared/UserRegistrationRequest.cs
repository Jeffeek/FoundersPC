using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.AuthorizationShared
{
    public class UserRegistrationRequest
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(ShortName = "Email", AutoGenerateFilter = true)]
        [Required]
        public string Email { get; set; }

        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters")]
        [Required]
        [DataType(DataType.Password)]
        public string RawPassword { get; set; }
    }
}
