using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.ApplicationShared.AccountSettings
{
    public class UserChangeLoginRequest
    {
        [Required]
        public int UserId { get; set; }

        [StringLength(maximumLength : 30,
                      MinimumLength = 5,
                      ErrorMessage = "Min 5, max 30")]
        [Required]
        public string NewLogin { get; set; }
    }
}
