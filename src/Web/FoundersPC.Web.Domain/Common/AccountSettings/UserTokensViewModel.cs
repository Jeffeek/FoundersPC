#region Using namespaces

using System.Collections.Generic;
using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Web.Domain.Common.AccountSettings
{
    public class UserTokensViewModel
    {
        public IEnumerable<AccessUserTokenReadDto> Tokens { get; set; }
    }
}