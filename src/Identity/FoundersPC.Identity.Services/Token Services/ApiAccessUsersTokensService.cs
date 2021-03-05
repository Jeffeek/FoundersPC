#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;

#endregion

namespace FoundersPC.Identity.Services.Token_Services
{
    public class ApiAccessUsersTokensService : IApiAccessUsersTokensService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;
        private readonly TokenEncryptorService _tokenEncryptorService;

        public ApiAccessUsersTokensService(IUnitOfWorkUsersIdentity unitOfWork, TokenEncryptorService tokenEncryptorService)
        {
            _unitOfWork = unitOfWork;
            _tokenEncryptorService = tokenEncryptorService;
        }

        #region IsTokenBlocked

        public async Task<bool> IsTokenBlockedAsync(string token)
        {
            var hashedToken = _tokenEncryptorService.ComputeHash(token);

            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByTokenAsync(hashedToken);

            return tokenEntity is not null && tokenEntity.IsBlocked;
        }

        public async Task<bool> IsTokenBlockedAsync(int id)
        {
            var token = await _unitOfWork.ApiAccessUsersTokensRepository.GetByIdAsync(id);

            return token is not null && token.IsBlocked;
        }

        #endregion

        #region IsTokenActive

        public async Task<bool> IsTokenActiveAsync(string token)
        {
            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByTokenAsync(token);

            if (ReferenceEquals(tokenEntity, null)) return false;

            return tokenEntity.ExpirationDate >= DateTime.Now && tokenEntity.ExpirationDate <= DateTime.Now;
        }

        public async Task<bool> IsTokenActiveAsync(int id)
        {
            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByIdAsync(id);

            if (ReferenceEquals(tokenEntity, null)) return false;

            return tokenEntity.ExpirationDate >= DateTime.Now && tokenEntity.ExpirationDate <= DateTime.Now;
        }

        #endregion

        #region Can make API request

        public async Task<bool> CanMakeRequestAsync(string token)
        {
            var allLogs = await _unitOfWork.AccessTokensLogsRepository.GetAllAsync();

            var accessTokenLog = allLogs.SingleOrDefault(log => log.ApiAccessToken.HashedToken == token);

            return CanMakeRequestAsync(accessTokenLog);
        }

        public async Task<bool> CanMakeRequestAsync(int tokenId)
        {
            var allLogs = await _unitOfWork.AccessTokensLogsRepository.GetAllAsync();

            var accessTokenLog = allLogs.SingleOrDefault(log => log.ApiAccessToken.Id == tokenId);

            return CanMakeRequestAsync(accessTokenLog);
        }

        private bool CanMakeRequestAsync(AccessTokenLog tokenLog)
        {
            if (ReferenceEquals(tokenLog, null)) return false;

            if (tokenLog.ApiAccessToken.IsBlocked) return false;

            return DateTime.Now.Ticks - tokenLog.RequestDateTime.Ticks >= 594530547;
        }

        #endregion

        #region Blocking

        public async Task<bool> BlockAsync(string token)
        {
            if (ReferenceEquals(token, null)) throw new ArgumentNullException(nameof(token));

            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByTokenAsync(token);

            if (tokenEntity is null) return false;

            return await BlockAsync(tokenEntity);
        }

        public async Task<bool> BlockAsync(int id)
        {
            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByIdAsync(id);

            if (tokenEntity is null) return false;

            return await BlockAsync(tokenEntity);
        }

        private async Task<bool> BlockAsync(ApiAccessUserToken token)
        {
            if (token.IsBlocked) return false;

            token.IsBlocked = false;
            token.ExpirationDate = DateTime.Now;

            return await _unitOfWork.ApiAccessUsersTokensRepository.UpdateAsync(token);
        }

        #endregion
    }
}