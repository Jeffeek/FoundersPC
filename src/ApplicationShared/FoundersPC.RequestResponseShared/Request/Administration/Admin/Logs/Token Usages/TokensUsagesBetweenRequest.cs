#region Using namespaces

using System;

#endregion

namespace FoundersPC.RequestResponseShared.Request.Administration.Admin.Logs.Token_Usages
{
    public class TokensUsagesBetweenRequest
    {
        public DateTime Start { get; set; }

        public DateTime Finish { get; set; }
    }
}