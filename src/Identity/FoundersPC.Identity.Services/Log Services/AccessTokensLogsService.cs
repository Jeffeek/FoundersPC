#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Dto;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.RequestResponseShared.Pagination.Response;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.Log_Services
{
    public class AccessTokensLogsService : IAccessTokensLogsService
    {
        private readonly ILogger<AccessTokensLogsService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public AccessTokensLogsService(IUnitOfWorkUsersIdentity unitOfWork,
                                       IMapper mapper,
                                       ILogger<AccessTokensLogsService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAllTokensLogsAsync() =>
            _mapper.Map<IEnumerable<AccessTokenLog>, IEnumerable<AccessTokenLogReadDto>>(await _unitOfWork
                                                                                               .AccessTokensLogsRepository
                                                                                               .GetAllAsync());

        public async Task<AccessTokenLogReadDto> GetTokenLogByIdAsync(int id) =>
            _mapper.Map<AccessTokenLog, AccessTokenLogReadDto>(await _unitOfWork.AccessTokensLogsRepository
                                                                                .GetByIdAsync(id));

        public async Task<IEnumerable<AccessTokenLogReadDto>> GetUsagesBetweenAsync(DateTime start, DateTime finish)
        {
            var logs = await _unitOfWork.AccessTokensLogsRepository.GetUsagesBetweenAsync(start, finish);

            return _mapper.Map<IEnumerable<AccessTokenLog>, IEnumerable<AccessTokenLogReadDto>>(logs);
        }

        public async Task<IEnumerable<AccessTokenLogReadDto>> GetUsagesInAsync(DateTime date)
        {
            var logs = await _unitOfWork.AccessTokensLogsRepository.GetUsagesInAsync(date);

            return _mapper.Map<IEnumerable<AccessTokenLog>, IEnumerable<AccessTokenLogReadDto>>(logs);
        }

        /// <inheritdoc/>
        public async Task<AccessTokenLogReadDto> GetLastTokenUsageAsync(int apiAccessTokenId) =>
            _mapper.Map<AccessTokenLog, AccessTokenLogReadDto>(await _unitOfWork.AccessTokensLogsRepository
                                                                                .GetLastTokenUsageAsync(apiAccessTokenId));

        /// <inheritdoc/>
        public async Task<AccessTokenLogReadDto> GetLastTokenUsageAsync(string apiAccessToken) =>
            _mapper.Map<AccessTokenLog, AccessTokenLogReadDto>(await _unitOfWork.AccessTokensLogsRepository
                                                                                .GetLastTokenUsageAsync(apiAccessToken));

        /// <inheritdoc/>
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetTokenLogsAsync(int tokenId) =>
            GetTokenLogsAsync(await _unitOfWork.AccessTokensRepository.GetByIdAsync(tokenId));

        /// <inheritdoc/>
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetTokenLogsAsync(string token) =>
            GetTokenLogsAsync(await _unitOfWork.AccessTokensRepository.GetByTokenAsync(token));

        /// <inheritdoc/>
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetUserTokenUsagesByUserIdAsync(int userId) =>
            _mapper.Map<IEnumerable<AccessTokenLog>,
                IEnumerable<AccessTokenLogReadDto>>(await _unitOfWork.AccessTokensLogsRepository
                                                                     .GetUserTokenUsagesByUserIdAsync(userId));

        /// <inheritdoc/>
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetUserTokenUsagesByUserEmailAsync(string userEmail) =>
            _mapper.Map<IEnumerable<AccessTokenLog>,
                IEnumerable<AccessTokenLogReadDto>>(await _unitOfWork.AccessTokensLogsRepository
                                                                     .GetUserTokenUsagesByUserEmailAsync(userEmail));

        public async Task<bool> LogAsync(int tokenId)
        {
            if (tokenId <= 0)
                return false;

            var token = await _unitOfWork.AccessTokensRepository.GetByIdAsync(tokenId);

            if (token == null)
                return false;

            var newLog = new AccessTokenLog
                         {
                             AccessTokenEntity = token,
                             ApiAccessUsersTokenId = tokenId,
                             RequestDateTime = DateTime.Now
                         };

            await _unitOfWork.AccessTokensLogsRepository.AddAsync(newLog);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        // 64 - length of the tokenEntity
        public async Task<bool> LogAsync(string token)
        {
            if (token == null
                || token.Length != 64)
            {
                _logger.LogWarning(token is null
                                       ? $"{nameof(AccessTokensLogsService)}: Log: tokenEntity was null"
                                       : $"{nameof(AccessTokensLogsService)}: Log: tokenEntity was with incorrect length! (length: {token.Length})");

                return false;
            }

            var tokenEntity = await _unitOfWork.AccessTokensRepository.GetByTokenAsync(token);

            if (tokenEntity == null)
            {
                _logger.LogWarning($"{nameof(AccessTokensLogsService)}: Log: {nameof(tokenEntity)} was not found in database");

                return false;
            }

            var newLog = new AccessTokenLog
                         {
                             AccessTokenEntity = tokenEntity,
                             ApiAccessUsersTokenId = tokenEntity.Id,
                             RequestDateTime = DateTime.Now
                         };

            await _unitOfWork.AccessTokensLogsRepository.AddAsync(newLog);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        #region Implementation of IPaginateableService<AccessTokenLogReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<AccessTokenLogReadDto>> GetPaginateableAsync(int pageNumber = 1,
                                                                                           int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<AccessTokenLog>,
                IEnumerable<AccessTokenLogReadDto>>(await _unitOfWork.AccessTokensLogsRepository
                                                                     .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWork.AccessTokensLogsRepository.CountAsync();

            return new PaginationResponse<AccessTokenLogReadDto>(items, totalItemsCount);
        }

        #endregion

        private IEnumerable<AccessTokenLogReadDto> GetTokenLogsAsync(AccessTokenEntity tokenEntity) =>
            _mapper.Map<IEnumerable<AccessTokenLog>, IEnumerable<AccessTokenLogReadDto>>(tokenEntity?.UsagesLogs ?? Enumerable.Empty<AccessTokenLog>());
    }
}