#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Dto;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.Token_Services
{
    public class ApiAccessUsersTokensService : IApiAccessUsersTokensService
    {
        private readonly ILogger<ApiAccessUsersTokensService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public ApiAccessUsersTokensService(IUnitOfWorkUsersIdentity unitOfWork,
                                           IMapper mapper,
                                           ILogger<ApiAccessUsersTokensService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokens(int userId)
        {
            var tokens = await _unitOfWork.ApiAccessUsersTokensRepository.GetAllUserTokens(userId);

            if (tokens is null)
                return null;

            return _mapper.Map<IEnumerable<ApiAccessUserToken>,
                IEnumerable<ApiAccessUserTokenReadDto>>(tokens);
        }

        public async Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokens(string userEmail)
        {
            var tokens = await _unitOfWork.ApiAccessUsersTokensRepository.GetAllUserTokens(userEmail);

            if (tokens is null)
                return null;

            return _mapper.Map<IEnumerable<ApiAccessUserToken>,
                IEnumerable<ApiAccessUserTokenReadDto>>(tokens);
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

        #region Can make API request

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

        #endregion

        #region Blocking

        public async Task<bool> BlockAsync(string token)
        {
            if (token is null)
            {
                _logger.LogError($"{nameof(ApiAccessUsersTokensService)}: token was null when tried to block");

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

            token.IsBlocked = false;
            token.ExpirationDate = DateTime.Now;

            return await _unitOfWork.ApiAccessUsersTokensRepository.UpdateAsync(token);
        }

        #endregion
    }
}