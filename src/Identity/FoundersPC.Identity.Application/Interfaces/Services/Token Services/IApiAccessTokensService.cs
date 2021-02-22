using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    [Obsolete]
    public interface IApiAccessTokensService
    {
        Task Generate(int countOfGenerated);
    }
}
