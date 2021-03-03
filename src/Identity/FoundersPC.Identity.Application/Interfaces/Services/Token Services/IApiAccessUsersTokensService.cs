#region Using namespaces

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IApiAccessUsersTokensService
    {
        Task<bool> IsTokenBlockedAsync(string token);

        Task<bool> IsTokenActiveAsync(string token);

        Task<bool> CanMakeRequestAsync(string token);

        Task<bool> BlockAsync(string token);

        Task<bool> IsTokenBlockedAsync(int id);

        Task<bool> IsTokenActiveAsync(int id);

        Task<bool> CanMakeRequestAsync(int tokenId);

        Task<bool> BlockAsync(int id);
    }
}