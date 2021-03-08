#region Using namespaces

using System.Collections.Generic;
using FoundersPC.AuthenticationShared;

#endregion

namespace FoundersPC.Web.Models.ViewModels
{
    public class UserTokensViewModel
    {
        public IEnumerable<ApiAccessUserTokenReadDto> Tokens { get; set; }
    }
}