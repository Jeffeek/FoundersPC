#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.DTO;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Log_Services
{
    public interface IUsersEntrancesService
    {
        Task<IEnumerable<UserEntranceLogReadDto>> GetAllAsync();

        Task<UserEntranceLogReadDto> GetByIdAsync(int id);

        Task<IEnumerable<UserEntranceLogReadDto>> GetEntrancesBetweenAsync(DateTime start, DateTime finish);

        Task<IEnumerable<UserEntranceLogReadDto>> GetEntrancesInAsync(DateTime date);

        Task<bool> LogAsync(int userId);
    }
}