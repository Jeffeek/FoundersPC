#region Using namespaces

using System.Threading.Tasks;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IApiAccessUsersTokensService
    {
        Task<bool> IsTokenBlockedAsync(string token);

        Task<bool> IsTokenActiveAsync(string token);

        Task<bool> CanMakeRequestAsync(string token);
    }
}