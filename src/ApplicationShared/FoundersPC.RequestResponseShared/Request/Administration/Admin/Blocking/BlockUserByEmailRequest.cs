using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.RequestResponseShared.Request.Administration.Admin.Blocking
{
    //todo: validation
    public class BlockUserByEmailRequest
    {
        public string UserEmail { get; set; }

        public bool BlockUserTokens { get; set; } = true;

        public bool SendNotificationToUserViaEmail { get; set; } = true;
    }
}
