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
    public class AccessTokensLogsService : IAccessTokensLogsService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public AccessTokensLogsService(IUnitOfWorkUsersIdentity unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<AccessTokenLog>> GetAllAsync() => await _unitOfWork.AccessTokensLogsRepository.GetAllAsync();

        public async Task<AccessTokenLog> GetByIdAsync(int id) => await _unitOfWork.AccessTokensLogsRepository.GetByIdAsync(id);

        public async Task<IEnumerable<AccessTokenLog>> GetUsagesBetweenAsync(DateTime start, DateTime finish)
        {
            var allLogs = await _unitOfWork.AccessTokensLogsRepository.GetAllAsync();

            var filtered = allLogs.Where(log => log.RequestDateTime >= start && log.RequestDateTime <= finish);

            return filtered;
        }

        public async Task<IEnumerable<AccessTokenLog>> GetUsagesInAsync(DateTime date)
        {
            var allLogs = await _unitOfWork.AccessTokensLogsRepository.GetAllAsync();

            var filtered = allLogs
                .Where(log => log.RequestDateTime.Year == date.Year
                              && log.RequestDateTime.Month == date.Month
                              && log.RequestDateTime.Day == date.Day);

            return filtered;
        }

        public async Task<bool> LogAsync(int tokenId)
        {
            if (tokenId <= 0) return false;

            var token = await _unitOfWork.ApiAccessUsersTokensRepository.GetByIdAsync(tokenId);

            if (token == null) return false;

            var newLog = new AccessTokenLog()
                         {
                             ApiAccessToken = token,
                             ApiAccessUsersTokenId = tokenId,
                             RequestDateTime = DateTime.Now
                         };

            await _unitOfWork.AccessTokensLogsRepository.AddAsync(newLog);

            return true;
        }

        public async Task<bool> LogAsync(string token)
        {
            if (token == null || token.Length != 88) return false;

            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByTokenAsync(token);

            if (tokenEntity == null) return false;

            var newLog = new AccessTokenLog()
                         {
                             ApiAccessToken = tokenEntity,
                             ApiAccessUsersTokenId = tokenEntity.Id,
                             RequestDateTime = DateTime.Now
                         };

            await _unitOfWork.AccessTokensLogsRepository.AddAsync(newLog);

            return true;
        }
    }
}