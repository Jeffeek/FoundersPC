using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Dto;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace FoundersPC.Identity.Services.Token_Services
{
    public class AccessTokensStatusService : IAccessTokensTokensStatusService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;
        private readonly ILogger<AccessTokensStatusService> _logger;

        public AccessTokensStatusService(IUnitOfWorkUsersIdentity unitOfWork,
                                         ILogger<AccessTokensStatusService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #region IsTokenBlocked

        public async Task<bool> IsTokenBlockedAsync(string token)
        {
            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByTokenAsync(token);

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

            if (tokenEntity is null)
                return false;

            return tokenEntity.ExpirationDate >= DateTime.Now && tokenEntity.ExpirationDate <= DateTime.Now;
        }

        public async Task<bool> IsTokenActiveAsync(int id)
        {
            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByIdAsync(id);

            if (tokenEntity is null)
                return false;

            return tokenEntity.ExpirationDate >= DateTime.Now && tokenEntity.ExpirationDate <= DateTime.Now;
        }

        #endregion
    }
}
