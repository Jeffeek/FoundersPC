#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Tokens
{
    public interface IUsersAccessTokensService
    {
        Task<IEnumerable<AccessTokenReadDto>> GetAllUsersAccessTokensAsync(string adminToken);

        Task<IPaginationResponse<AccessTokenReadDto>> GetPaginateableTokensAsync(int pageNumber, int pageSize, string adminToken);

        Task<bool> BlockTokenByIdAsync(int tokenId, string adminToken);

        Task<bool> BlockTokenByStringTokenAsync(string token, string adminToken);
    }
}