using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

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

        public async Task<bool> BlockAsync(string token)
        {
            if (token is null)
            {
                _logger.LogError($"{nameof(AccessUsersTokensService)}: string token was null when tried to block");

                throw new ArgumentNullException(nameof(token));
            }

            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByTokenAsync(token);

            if (tokenEntity is null)
                return false;

            return await BlockAsync(tokenEntity);
        }

        public async Task<bool> BlockAsync(int id)
        {
            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByIdAsync(id);

            if (tokenEntity is null)
                return false;

            return await BlockAsync(tokenEntity);
        }

        private async Task<bool> BlockAsync(ApiAccessUserToken token)
        {
            if (token.IsBlocked)
                return false;

            token.IsBlocked = true;
            token.ExpirationDate = DateTime.Now;

            return await _unitOfWork.ApiAccessUsersTokensRepository.UpdateAsync(token) && await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
