#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Tokens
{
    public interface IAccessTokensRepository : IRepositoryAsync<AccessTokenEntity>,
                                               IPaginateableRepository<AccessTokenEntity>
    {
        Task<AccessTokenEntity> GetByTokenAsync(string token);

        Task<IEnumerable<AccessTokenEntity>> GetAllUserTokensAsync(int userId);

        Task<IEnumerable<AccessTokenEntity>> GetAllUserTokensAsync(string userEmail);
    }
}