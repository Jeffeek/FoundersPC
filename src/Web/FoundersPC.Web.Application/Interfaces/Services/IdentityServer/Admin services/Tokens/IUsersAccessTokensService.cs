using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Dto;

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Tokens
{
    public interface IUsersAccessTokensService
    {
        Task<IEnumerable<ApiAccessUserTokenReadDto>> GetAllUsersAccessTokensAsync(string adminToken);

        Task<IEnumerable<ApiAccessUserTokenReadDto>> GetPaginateableTokensAsync(int pageNumber, int pageSize, string adminToken);

        Task<bool> BlockTokenByIdAsync(int tokenId, string adminToken);

        Task<bool> BlockTokenByStringTokenAsync(string token, string adminToken);
    }
}