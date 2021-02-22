using System;
using System.Threading.Tasks;

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    [Obsolete]
    public interface IApiAccessTokensService
    {
        Task Generate(int countOfGenerated);
    }
}
