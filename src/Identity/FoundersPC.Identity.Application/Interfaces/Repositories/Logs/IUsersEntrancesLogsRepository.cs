#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Logs
{
    public interface IUsersEntrancesLogsRepository : IRepositoryAsync<UserEntranceLog>
    {
        Task<IEnumerable<UserEntranceLog>> GetEntrancesBetweenAsync(DateTime start, DateTime finish);

        Task<IEnumerable<UserEntranceLog>> GetEntrancesInAsync(DateTime date);

        Task<IEnumerable<UserEntranceLog>> GetUserEntrancesAsync(int userId);

        Task<IEnumerable<UserEntranceLog>> GetUserEntrancesAsync(string userEmail);
    }
}