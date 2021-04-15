using System.Threading.Tasks;

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IAccessTokensRequestsService
    {
        Task<bool> CanMakeRequestAsync(string token);

        Task<bool> CanMakeRequestAsync(int tokenId);
    }
}
