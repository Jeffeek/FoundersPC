using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Infrastructure.UnitOfWork;

namespace FoundersPC.Identity.Services.Log_Services
{
    public class AccessTokensLogsService : IAccessTokensLogsService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public AccessTokensLogsService(IUnitOfWorkUsersIdentity unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<AccessTokenLog>> GetAll() =>
            await _unitOfWork.AccessTokensLogsRepository.GetAllAsync();

        public async Task<AccessTokenLog> Get(int id) =>
            await _unitOfWork.AccessTokensLogsRepository.GetByIdAsync(id);

        public async Task<IEnumerable<AccessTokenLog>> GetUsagesBetween(DateTime start, DateTime finish)
        {
            var allLogs = await _unitOfWork.AccessTokensLogsRepository.GetAllAsync();

            var filtered = allLogs.Where(log => log.RequestDateTime >= start && log.RequestDateTime <= finish);

            return filtered;
        }

        public async Task<IEnumerable<AccessTokenLog>> GetUsagesIn(DateTime date)
        {
            var allLogs = await _unitOfWork.AccessTokensLogsRepository.GetAllAsync();

            var filtered = allLogs
                .Where(log => log.RequestDateTime.Year == date.Year
                              && log.RequestDateTime.Month == date.Month
                              && log.RequestDateTime.Day == date.Day);

            return filtered;
        }
    }
}
