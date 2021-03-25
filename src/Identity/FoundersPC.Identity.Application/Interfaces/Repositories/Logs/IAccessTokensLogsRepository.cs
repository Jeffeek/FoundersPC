#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Logs
{
    public interface IAccessTokensLogsRepository : IRepositoryAsync<AccessTokenLog>
    {
        Task<IEnumerable<AccessTokenLog>> GetUsagesBetweenAsync(DateTime start, DateTime finish);

        Task<IEnumerable<AccessTokenLog>> GetUsagesInAsync(DateTime date);
    }
}