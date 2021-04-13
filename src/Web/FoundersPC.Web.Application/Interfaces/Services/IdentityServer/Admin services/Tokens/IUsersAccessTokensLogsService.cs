#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Tokens
{
    public interface IUsersAccessTokensLogsService
    {
        Task<IEnumerable<AccessTokenLogReadDto>> GetAllAccessTokensLogsAsync(string adminToken);

        Task<IEnumerable<AccessTokenLogReadDto>> GetPaginateableAccessTokensLogsAsync(int pageNumber,
                                                                                      int pageSize,
                                                                                      string adminToken);

        Task<IEnumerable<AccessTokenLogReadDto>> GetAllAccessTokensLogsByUserId(int userId, string adminToken);

        Task<IEnumerable<AccessTokenLogReadDto>> GetAllUserAccessTokensLogsByUserEmail(string userEmail,
                                                                                       string adminToken);
    }
}