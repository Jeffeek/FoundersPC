using System.Threading.Tasks;

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IAccessTokensBlockingService
    {
        Task<bool> BlockAsync(string token);

        Task<bool> BlockAsync(int id);
    }
}
