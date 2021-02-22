using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Domain.Entities.Tokens;

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Tokens
{
    public interface IApiAccessTokensRepository : IRepositoryAsync<ApiAccessToken> { }
}
