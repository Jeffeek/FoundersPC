#region Using namespaces

using System.Collections.Generic;
using FoundersPC.WebIdentityShared;

#endregion

namespace FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings
{
    public class UserTokensViewModel
    {
        public IEnumerable<ApplicationAccessToken> Tokens { get; set; }
    }
}