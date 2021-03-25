#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
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

        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<AccessTokenLog>, IEnumerable<AccessTokenLogReadDto>>(await _unitOfWork
                .AccessTokensLogsRepository.GetAllAsync());

        public async Task<AccessTokenLogReadDto> GetByIdAsync(int id) =>
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

        public async Task<bool> LogAsync(int tokenId)
        {
            if (tokenId <= 0) return false;

            var token = await _unitOfWork.ApiAccessUsersTokensRepository.GetByIdAsync(tokenId);

            if (token == null) return false;

            var newLog = new AccessTokenLog
                         {
                             ApiAccessToken = token,
                             ApiAccessUsersTokenId = tokenId,
                             RequestDateTime = DateTime.Now
                         };

            await _unitOfWork.AccessTokensLogsRepository.AddAsync(newLog);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        // 88 - length of the token
        public async Task<bool> LogAsync(string token)
        {
            if (token == null
                || token.Length != 88)
            {
                _logger.LogWarning(token is null
                                       ? $"{nameof(AccessTokensLogsService)}: Log: token was null"
                                       : $"{nameof(AccessTokensLogsService)}: Log: token was with incorrect length! (length: {token.Length})");

                return false;
            }

            var tokenEntity = await _unitOfWork.ApiAccessUsersTokensRepository.GetByTokenAsync(token);

            if (tokenEntity == null)
            {
                _logger.LogWarning($"{nameof(AccessTokensLogsService)}: Log: {nameof(tokenEntity)} was not found in database");

                return false;
            }

            var newLog = new AccessTokenLog
                         {
                             ApiAccessToken = tokenEntity,
                             ApiAccessUsersTokenId = tokenEntity.Id,
                             RequestDateTime = DateTime.Now
                         };

            await _unitOfWork.AccessTokensLogsRepository.AddAsync(newLog);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}