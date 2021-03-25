#region Using namespaces

using System;

#endregion

namespace FoundersPC.RequestResponseShared.Request.Administration.Admin.Logs.Entrances
{
    public class UsersEntrancesBetweenRequest
    {
        public DateTime Start { get; set; }

        public DateTime Finish { get; set; }
    }
}