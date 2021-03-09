﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
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
        private readonly IMapper _mapper;
        private readonly TokenEncryptorService _tokenEncryptorService;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public ApiAccessUsersTokensService(IUnitOfWorkUsersIdentity unitOfWork,
                                           TokenEncryptorService tokenEncryptorService,
                                           IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _tokenEncryptorService = tokenEncryptorService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokens(int userId)
        {
            var tokens = await _unitOfWork.ApiAccessUsersTokensRepository.GetAllUserTokens(userId);

            if (tokens is null) return null;

            return _mapper.Map<IEnumerable<ApiAccessUserToken>,
                IEnumerable<ApiAccessUserTokenReadDto>>(tokens);
        }

        public async Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokens(string userEmail)
        {
            var tokens = await _unitOfWork.ApiAccessUsersTokensRepository.GetAllUserTokens(userEmail);

            if (tokens is null) return null;

            return _mapper.Map<IEnumerable<ApiAccessUserToken>,
                IEnumerable<ApiAccessUserTokenReadDto>>(tokens);
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