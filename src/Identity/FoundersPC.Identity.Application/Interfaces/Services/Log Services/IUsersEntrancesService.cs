#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Domain.Entities.Logs;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Log_Services
{
    public interface IUsersEntrancesService
    {
        Task<IEnumerable<UserEntranceLog>> GetAllAsync();

        Task<UserEntranceLog> GetByIdAsync(int id);

        Task<IEnumerable<UserEntranceLog>> GetEntrancesBetweenAsync(DateTime start, DateTime finish);

        Task<IEnumerable<UserEntranceLog>> GetEntrancesInAsync(DateTime date);

        Task<bool> LogAsync(int userId);
    }
}