#region Using namespaces

using System;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.RequestResponseShared.Request.Administration.Admin.Logs.Entrances
{
    public class UsersEntrancesBetweenRequest
    {
        [FromQuery(Name = "Start")]
        public DateTime Start { get; set; }

        [FromQuery(Name = "Finish")]
        public DateTime Finish { get; set; }
    }
}