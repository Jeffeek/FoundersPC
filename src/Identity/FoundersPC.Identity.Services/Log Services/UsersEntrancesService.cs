#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Infrastructure.UnitOfWork;

#endregion

namespace FoundersPC.Identity.Services.Log_Services
{
    public class UsersEntrancesService : IUsersEntrancesService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public UsersEntrancesService(IUnitOfWorkUsersIdentity unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<UserEntranceLog>> GetAll() => await _unitOfWork.UsersEntrancesLogsRepository.GetAllAsync();

        public async Task<UserEntranceLog> Get(int id) => await _unitOfWork.UsersEntrancesLogsRepository.GetByIdAsync(id);

        public async Task<IEnumerable<UserEntranceLog>> GetEntrancesBetween(DateTime start, DateTime finish)
        {
            var allLogs = await _unitOfWork.UsersEntrancesLogsRepository.GetAllAsync();

            var filtered = allLogs
                .Where(log => log.Entrance >= start && log.Entrance <= finish);

            return filtered;
        }

        public async Task<IEnumerable<UserEntranceLog>> GetEntrancesIn(DateTime date)
        {
            var allLogs = await _unitOfWork.UsersEntrancesLogsRepository.GetAllAsync();

            var filtered = allLogs
                .Where(log => log.Entrance.Year == date.Year
                              && log.Entrance.Month == date.Month
                              && log.Entrance.Day == date.Day);

            return filtered;
        }
    }
}