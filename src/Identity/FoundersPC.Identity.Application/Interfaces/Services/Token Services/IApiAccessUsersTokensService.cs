#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.RepositoryShared.Repository;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    // todo: maybe separate by ISP
    public interface IApiAccessUsersTokensService : IPaginateableService<ApiAccessUserTokenReadDto>
    {
        Task<IEnumerable<ApiAccessUserTokenReadDto>> GetAllTokensAsync();

        Task<bool> IsTokenBlockedAsync(string token);

        Task<bool> IsTokenActiveAsync(string token);

        Task<bool> CanMakeRequestAsync(string token);

        Task<bool> BlockAsync(string token);

        Task<bool> IsTokenBlockedAsync(int id);

        Task<bool> IsTokenActiveAsync(int id);

        Task<bool> CanMakeRequestAsync(int tokenId);

        Task<bool> BlockAsync(int id);

        Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokensAsync(int userId);

        Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokensAsync(string userEmail);
    }
}