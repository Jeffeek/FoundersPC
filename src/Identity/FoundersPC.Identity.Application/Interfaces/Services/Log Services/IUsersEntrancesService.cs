#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Log_Services
{
    public interface IUsersEntrancesService : IPaginateableService<UserEntranceLogReadDto>
    {
        Task<IEnumerable<UserEntranceLogReadDto>> GetAllAsync();

        Task<UserEntranceLogReadDto> GetByIdAsync(int id);

        Task<IEnumerable<UserEntranceLogReadDto>> GetEntrancesBetweenAsync(DateTime start, DateTime finish);

        Task<IEnumerable<UserEntranceLogReadDto>> GetEntrancesInAsync(DateTime date);

        Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesAsync(int userId);

        Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrancesAsync(string userEmail);

        Task<bool> LogAsync(int userId);
    }
}