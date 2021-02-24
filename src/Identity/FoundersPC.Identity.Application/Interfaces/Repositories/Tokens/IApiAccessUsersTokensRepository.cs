#region Using namespaces

using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Domain.Entities.Tokens;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Tokens
{
    public interface IApiAccessUsersTokensRepository : IRepositoryAsync<ApiAccessUserToken> { }
}