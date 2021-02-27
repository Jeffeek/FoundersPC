#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Infrastructure.UnitOfWork;

#endregion

namespace FoundersPC.Identity.Services.Token_Services
{
    public class ApiAccessUsersTokensService : IApiAccessUsersTokensService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public ApiAccessUsersTokensService(IUnitOfWorkUsersIdentity unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> IsTokenBlockedAsync(string token)
        {
            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByTokenAsync(token);

            if (ReferenceEquals(tokenEntity, null)) throw new KeyNotFoundException(nameof(tokenEntity));

            return tokenEntity.IsBlocked;
        }

        public async Task<bool> IsTokenActiveAsync(string token)
        {
            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByTokenAsync(token);

            if (ReferenceEquals(tokenEntity, null)) throw new KeyNotFoundException(nameof(tokenEntity));

            return tokenEntity.ExpirationDate >= DateTime.Now && tokenEntity.ExpirationDate <= DateTime.Now;
        }

        public async Task<bool> CanMakeRequestAsync(string token)
        {
            var allLogs = await _unitOfWork.AccessTokensLogsRepository.GetAllAsync();

            var tokenEntity = allLogs.SingleOrDefault(log => log.ApiAccessToken.HashedToken == token);

            if (ReferenceEquals(tokenEntity, null)) return false;

            return DateTime.Now.Ticks - tokenEntity.RequestDateTime.Ticks >= 594530547;
        }
    }
}