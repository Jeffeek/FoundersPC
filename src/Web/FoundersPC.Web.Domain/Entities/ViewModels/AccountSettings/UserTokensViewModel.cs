#region Using namespaces

using FoundersPC.ApplicationShared;
using System.Collections.Generic;

#endregion

namespace FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings
{
    public class UserTokensViewModel
    {
        public IEnumerable<ApiAccessUserTokenReadDto> Tokens { get; set; }
    }
}