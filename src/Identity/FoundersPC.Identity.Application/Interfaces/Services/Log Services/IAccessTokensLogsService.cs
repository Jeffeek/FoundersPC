using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Domain.Entities.Logs;

namespace FoundersPC.Identity.Application.Interfaces.Services.Log_Services
{
    public interface IAccessTokensLogsService
    {
        Task<IEnumerable<AccessTokenLog>> GetAll();

        Task<AccessTokenLog> Get(int id);

        Task<IEnumerable<AccessTokenLog>> GetUsagesBetween(DateTime start, DateTime finish);

        Task<IEnumerable<AccessTokenLog>> GetUsagesIn(DateTime date);
    }
}
