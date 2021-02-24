#region Using namespaces

using System.Threading.Tasks;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IApiAccessUsersTokensService
    {
        Task<bool> IsTokenBlocked(string token);

        Task<bool> IsTokenActive(string token);

        Task<bool> CanMakeRequest(string token);
    }
}