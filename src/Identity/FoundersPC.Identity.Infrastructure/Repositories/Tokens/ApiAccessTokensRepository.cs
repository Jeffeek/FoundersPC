using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories.Tokens;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Identity.Infrastructure.Repositories.Tokens
{
    public class ApiAccessTokensRepository : GenericRepositoryAsync<ApiAccessToken>, IApiAccessTokensRepository
    {
        public ApiAccessTokensRepository(FoundersPCUsersContext context) : base(context) { }

        public async Task<IEnumerable<ApiAccessToken>> GetAllAsync() =>
            await Context.Set<ApiAccessToken>()
                         .ToListAsync();
    }
}
