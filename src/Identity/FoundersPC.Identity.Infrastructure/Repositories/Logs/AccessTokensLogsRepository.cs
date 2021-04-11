#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Repositories.Logs;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories.Logs
{
    public class AccessTokensLogsRepository : GenericRepositoryAsync<AccessTokenLog>, IAccessTokensLogsRepository
    {
        public AccessTokensLogsRepository(DbContext context) : base(context) { }

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

        /// <inheritdoc/>
        public async Task<AccessTokenLog> GetLastTokenUsageAsync(int apiAccessTokenId) =>
            await Context.Set<AccessTokenLog>()
                         .Where(x => x.ApiAccessUsersTokenId == apiAccessTokenId)
                         .OrderBy(x => x.Id)
                         .LastOrDefaultAsync();

        /// <inheritdoc/>
        public async Task<AccessTokenLog> GetLastTokenUsageAsync(string apiAccessToken) =>
            await Context.Set<AccessTokenLog>()
                         .Include(x => x.ApiAccessToken)
                         .Where(x => x.ApiAccessToken.HashedToken == apiAccessToken)
                         .OrderBy(x => x.Id)
                         .LastOrDefaultAsync();

        #region Implementation of IPaginateableRepository<AccessTokenLog>

        /// <inheritdoc/>
        public async Task<IEnumerable<AccessTokenLog>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await Context.Set<AccessTokenLog>()
                         .Paginate(pageNumber, pageSize)
                         .Include(x => x.ApiAccessToken)
                         .ToListAsync();

        #endregion
    }
}