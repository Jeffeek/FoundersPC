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
    public class AccessTokensRequestsService : IAccessTokensRequestsService
    {
        private readonly ILogger<AccessTokensRequestsService> _logger;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public AccessTokensRequestsService(ILogger<AccessTokensRequestsService> logger,
                                           IUnitOfWorkUsersIdentity unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CanMakeRequestAsync(string token)
        {
            var tokenEntity = await _unitOfWork.AccessTokensRepository.GetByTokenAsync(token);

            return await CanMakeRequestAsync(tokenEntity);
        }

        public async Task<bool> CanMakeRequestAsync(int tokenId)
        {
            var tokenEntity = await _unitOfWork.AccessTokensRepository.GetByIdAsync(tokenId);

            return await CanMakeRequestAsync(tokenEntity);
        }

        private async Task<bool> CanMakeRequestAsync(AccessTokenEntity tokenEntity)
        {
            if (tokenEntity is null)
                return false;

            var lastUsageLog = await _unitOfWork.AccessTokensLogsRepository.GetLastTokenUsageAsync(tokenEntity.Id);

            if (lastUsageLog is null)
                return true;

            var now = DateTime.Now;

            return !tokenEntity.IsBlocked
                   && tokenEntity.ExpirationDate > now
                   && now.Ticks - lastUsageLog.RequestDateTime.Ticks
                   >= TimeSpan.TicksPerMinute; // the request to API can be made 1 time per 1 minute
        }
    }
}