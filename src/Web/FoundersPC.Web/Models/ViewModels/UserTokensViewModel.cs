using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.AuthenticationShared;

namespace FoundersPC.Web.Models.ViewModels
{
    public class UserTokensViewModel
    {
        public IEnumerable<ApiAccessUserTokenReadDto> Tokens { get; set; }
    }
}
