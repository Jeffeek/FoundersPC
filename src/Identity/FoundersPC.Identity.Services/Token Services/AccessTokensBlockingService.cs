#region Using namespaces

using System;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.Token_Services
{
    public class AccessTokensBlockingService : IAccessTokensBlockingService
    {
        private readonly ILogger<AccessTokensBlockingService> _logger;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public AccessTokensBlockingService(ILogger<AccessTokensBlockingService> logger,
                                           IUnitOfWorkUsersIdentity unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <exception cref="T:System.ArgumentNullException"><paramref name="token"/> is <see langword="null"/></exception>
        public async Task<bool> BlockAsync(string token)
        {
            if (token is null)
            {
                _logger.LogError($"{nameof(AccessUsersTokensService)}: string token was null when tried to block");

                throw new ArgumentNullException(nameof(token));
            }

            var tokenEntity = await _unitOfWork.AccessTokensRepository.GetByTokenAsync(token);

            // todo logger
            if (tokenEntity is null)
                return false;

            return await BlockAsync(tokenEntity);
        }

        public async Task<bool> BlockAsync(int id)
        {
            var tokenEntity = await _unitOfWork.AccessTokensRepository.GetByIdAsync(id);

            // todo logger
            if (tokenEntity is null)
                return false;

            return await BlockAsync(tokenEntity);
        }

        /// <inheritdoc/>
        public async Task<bool> UnBlockAsync(string token)
        {
            if (token is null)
            {
                _logger.LogError($"{nameof(AccessUsersTokensService)}: string token was null when tried to block");

                throw new ArgumentNullException(nameof(token));
            }

            var tokenEntity = await _unitOfWork.AccessTokensRepository.GetByTokenAsync(token);

            // todo logger
            if (tokenEntity is null)
                return false;

            return await UnBlockAsync(tokenEntity);
        }

        /// <inheritdoc/>
        public async Task<bool> UnBlockAsync(int id)
        {
            var tokenEntity = await _unitOfWork.AccessTokensRepository.GetByIdAsync(id);

            // todo logger
            if (tokenEntity is null)
                return false;

            return await UnBlockAsync(tokenEntity);
        }

        private async Task<bool> BlockAsync(AccessTokenEntity tokenEntity)
        {
            if (tokenEntity.IsBlocked)
                return false;

            tokenEntity.IsBlocked = true;

            return await _unitOfWork.AccessTokensRepository.UpdateAsync(tokenEntity) && await _unitOfWork.SaveChangesAsync() > 0;
        }

        private async Task<bool> UnBlockAsync(AccessTokenEntity tokenEntity)
        {
            if (!tokenEntity.IsBlocked)
                return false;

            tokenEntity.IsBlocked = false;

            return await _unitOfWork.AccessTokensRepository.UpdateAsync(tokenEntity) && await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}