﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Logs
{
    public interface IAccessTokensLogsRepository : IRepositoryAsync<AccessTokenLog>,
                                                   IPaginateableRepository<AccessTokenLog>
    {
        Task<IEnumerable<AccessTokenLog>> GetUsagesBetweenAsync(DateTime start, DateTime finish);

        Task<IEnumerable<AccessTokenLog>> GetUsagesInAsync(DateTime date);

        Task<AccessTokenLog> GetLastTokenUsageAsync(int apiAccessTokenId);

        Task<AccessTokenLog> GetLastTokenUsageAsync(string apiAccessToken);

        Task<IEnumerable<AccessTokenLog>> GetUserTokenUsagesByUserIdAsync(int userId);

        Task<IEnumerable<AccessTokenLog>> GetUserTokenUsagesByUserEmailAsync(string userEmail);
    }
}