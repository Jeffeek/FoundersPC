using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories.Logs;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Identity.Infrastructure.Repositories.Logs
{
    public class AccessTokensLogsRepository : GenericRepositoryAsync<AccessTokenLog>, IAccessTokensLogsRepository
    {
        public AccessTokensLogsRepository(FoundersPCUsersContext context) : base(context) { }

        public override async Task<IEnumerable<AccessTokenLog>> GetAllAsync() =>
            await Context.Set<AccessTokenLog>()
                         .Include(log => log.ApiAccessToken)
                         .ToListAsync();
    }
}
