using System.Threading.Tasks;

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IAccessTokensTokensStatusService
    {
        Task<bool> IsTokenBlockedAsync(string token);

        Task<bool> IsTokenBlockedAsync(int id);

        Task<bool> IsTokenActiveAsync(string token);

        Task<bool> IsTokenActiveAsync(int id);
    }
}
