using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.AuthorizationShared
{
    public class UserRegistrationResponse
    {
        public bool SuccessfulRegistration { get; set; } = false;

        public string Email { get; set; } = null;

        public string RoleTitle { get; set; } = "None";
    }
}
