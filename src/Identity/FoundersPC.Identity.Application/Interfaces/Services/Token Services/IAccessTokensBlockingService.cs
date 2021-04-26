#region Using namespaces

using System.Threading.Tasks;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IAccessTokensBlockingService
    {
        Task<bool> BlockAsync(string token);

        Task<bool> BlockAsync(int id);

        Task<bool> UnBlockAsync(string token);

        Task<bool> UnBlockAsync(int id);
    }
}