using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.RequestResponseShared.Response.Administration.Admin.Blocking
{
    public class UnblockUserResponse
    {
        public string AdministratorEmail { get; set; }

        public bool IsUnblockingSuccessful { get; set; } = false;

        public string Error { get; set; } = null;
    }
}
