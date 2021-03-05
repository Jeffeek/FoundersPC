#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Domain.Entities.Tokens;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Tokens
{
    public interface IApiAccessUsersTokensRepository : IRepositoryAsync<ApiAccessUserToken>
    {
        Task<ApiAccessUserToken> GetByTokenAsync(string token);

        Task<IEnumerable<ApiAccessUserToken>> GetAllUserTokens(int userId);
    }
}