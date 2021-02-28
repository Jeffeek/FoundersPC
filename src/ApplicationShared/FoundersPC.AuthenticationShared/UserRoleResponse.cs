using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.AuthenticationShared
{
    public class UserRoleResponse
    {
        public bool IsSucceed { get; set; } = false;

        public string Exception { get; set; } = null;

        public string Response { get; set; } = null;
    }
}
