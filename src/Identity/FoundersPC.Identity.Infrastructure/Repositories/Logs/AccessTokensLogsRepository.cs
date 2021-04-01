#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Repositories.Logs;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.Contexts;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories.Logs
{
    public class AccessTokensLogsRepository : GenericRepositoryAsync<AccessTokenLog>, IAccessTokensLogsRepository
    {
        public AccessTokensLogsRepository(FoundersPCUsersContext context) : base(context) { }

        public override async Task<IEnumerable<AccessTokenLog>> GetAllAsync() =>
            await Context.Set<AccessTokenLog>()
                         .Include(log => log.ApiAccessToken)
                         .ToListAsync();

        public async Task<IEnumerable<AccessTokenLog>> GetUsagesBetweenAsync(DateTime start, DateTime finish) =>
            await Context.Set<AccessTokenLog>()
                         .Where(log => log.RequestDateTime >= start && log.RequestDateTime <= finish)
                         .ToListAsync();

        public async Task<IEnumerable<AccessTokenLog>> GetUsagesInAsync(DateTime date) =>
            await Context.Set<AccessTokenLog>()
                         .Where(log => log.RequestDateTime.Year == date.Year
                                       && log.RequestDateTime.Month == date.Month
                                       && log.RequestDateTime.Day == date.Day)
                         .ToListAsync();
    }
}