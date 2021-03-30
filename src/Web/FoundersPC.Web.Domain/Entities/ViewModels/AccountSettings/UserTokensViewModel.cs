#region Using namespaces

using System.Collections.Generic;
using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings
{
    public class UserTokensViewModel
    {
        public IEnumerable<ApiAccessUserTokenReadDto> Tokens { get; set; }
    }
}