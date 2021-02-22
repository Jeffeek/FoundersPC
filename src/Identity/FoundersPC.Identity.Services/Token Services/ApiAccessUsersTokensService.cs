using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Infrastructure.UnitOfWork;

namespace FoundersPC.Identity.Services.Token_Services
{
    public class ApiAccessUsersTokensService : IApiAccessUsersTokensService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public ApiAccessUsersTokensService(IUnitOfWorkUsersIdentity unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsTokenBlocked(string token)
        {
            var allTokens = await _unitOfWork.ApiAccessUsersTokensRepository.GetAllAsync();

            var tokenEntity = allTokens.FirstOrDefault(t => t.Token.HashedToken == token);

            if (ReferenceEquals(tokenEntity, null)) throw new KeyNotFoundException(nameof(tokenEntity));

            return tokenEntity.IsBlocked;
        }

        public async Task<bool> IsTokenActive(string token)
        {
            var allTokens = await _unitOfWork.ApiAccessUsersTokensRepository.GetAllAsync();

            var tokenEntity = allTokens.FirstOrDefault(t => t.Token.HashedToken == token);

            if (ReferenceEquals(tokenEntity, null)) throw new KeyNotFoundException(nameof(tokenEntity));

            return tokenEntity.ExpirationDate >= DateTime.Now && tokenEntity.ExpirationDate <= DateTime.Now;
        }

        public async Task<bool> CanMakeRequest(string token)
        {
            var allLogs = await _unitOfWork.AccessTokensLogsRepository.GetAllAsync();

            var tokenEntity = allLogs.SingleOrDefault(log => log.ApiAccessToken.Token.HashedToken == token);

            if (ReferenceEquals(tokenEntity, null)) return false;

            return DateTime.Now.Ticks - tokenEntity.RequestDateTime.Ticks >= 594530547;
        }
    }
}
