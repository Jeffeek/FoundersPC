#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Response.Pagination;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Tokens
{
    public interface IUsersAccessTokensLogsService
    {
        Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsAsync(string adminToken);

        Task<IPaginationResponse<AccessTokenLogReadDto>> GetPaginateableAccessTokensLogsAsync(int pageNumber,
                                                                                              int pageSize,
                                                                                              string adminToken);

        Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByUserIdAsync(int userId, string adminToken);

        Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByUserEmailAsync(string userEmail,
                                                                                     string adminToken);

        Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByTokenIdAsync(int tokenId, string adminToken);

        Task<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByTokenAsync(string token, string adminToken);
    }
}