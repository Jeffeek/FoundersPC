using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories.Tokens;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Identity.Infrastructure.Repositories.Tokens
{
    public class ApiAccessUsersTokensRepository : GenericRepositoryAsync<ApiAccessUserToken>, IApiAccessUsersTokensRepository
    {
        public ApiAccessUsersTokensRepository(FoundersPCUsersContext context) : base(context) { }

        public override async Task<IEnumerable<ApiAccessUserToken>> GetAllAsync() =>
            await Context.Set<ApiAccessUserToken>()
                         .Include(token => token.User)
                         .ThenInclude(user => user.Role)
                         .ToListAsync();
    }
}
