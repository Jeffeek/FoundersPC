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
    public class AccessTokensRequestsService : IAccessTokensRequestsService
    {
        private readonly Logger<AccessTokensRequestsService> _logger;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public AccessTokensRequestsService(Logger<AccessTokensRequestsService> logger,
                                           IUnitOfWorkUsersIdentity unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CanMakeRequestAsync(string token)
        {
            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByTokenAsync(token);

            return await CanMakeRequestAsync(tokenEntity);
        }

        public async Task<bool> CanMakeRequestAsync(int tokenId)
        {
            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByIdAsync(tokenId);

            return await CanMakeRequestAsync(tokenEntity);
        }

        private async Task<bool> CanMakeRequestAsync(ApiAccessUserToken token)
        {
            if (token is null)
                return false;

            var lastUsageLog = await _unitOfWork.AccessTokensLogsRepository.GetLastTokenUsageAsync(token.Id);

            if (lastUsageLog is null)
                return true;

            var now = DateTime.Now;

            return !token.IsBlocked
                   && token.ExpirationDate > now
                   && now.Ticks - lastUsageLog.RequestDateTime.Ticks
                   >= TimeSpan.TicksPerMinute; // the request to API can be made 1 time per 1 minute
        }
    }
}
