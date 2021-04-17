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
                _logger.LogError($"{nameof(AccessUsersTokensService)}: string tokenEntity was null when tried to block");

                throw new ArgumentNullException(nameof(token));
            }

            var tokenEntity = await _unitOfWork.AccessTokensRepository.GetByTokenAsync(token);

            if (tokenEntity is null)
                return false;

            return await BlockAsync(tokenEntity);
        }

        public async Task<bool> BlockAsync(int id)
        {
            var tokenEntity = await _unitOfWork.AccessTokensRepository.GetByIdAsync(id);

            if (tokenEntity is null)
                return false;

            return await BlockAsync(tokenEntity);
        }

        private async Task<bool> BlockAsync(AccessTokenEntity tokenEntity)
        {
            if (tokenEntity.IsBlocked)
                return false;

            tokenEntity.IsBlocked = true;

            return await _unitOfWork.AccessTokensRepository.UpdateAsync(tokenEntity) && await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}