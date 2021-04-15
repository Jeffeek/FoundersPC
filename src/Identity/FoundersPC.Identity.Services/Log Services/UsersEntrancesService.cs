#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Dto;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.RequestResponseShared.Response.Pagination;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.Log_Services
{
    public class UsersEntrancesService : IUsersEntrancesService
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<UsersEntrancesService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public UsersEntrancesService(IUnitOfWorkUsersIdentity unitOfWork,
                                     IEmailService emailService,
                                     ILogger<UsersEntrancesService> logger,
                                     IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<UserEntranceLog>, IEnumerable<UserEntranceLogReadDto>>(await _unitOfWork
                                                                                                 .UsersEntrancesLogsRepository
                                                                                                 .GetAllAsync());

        public async Task<UserEntranceLogReadDto> GetByIdAsync(int id) =>
            _mapper.Map<UserEntranceLog, UserEntranceLogReadDto>(await _unitOfWork.UsersEntrancesLogsRepository
                                                                                  .GetByIdAsync(id));

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetEntrancesBetweenAsync(DateTime start, DateTime finish)
        {
            var logs = await _unitOfWork.UsersEntrancesLogsRepository.GetEntrancesBetweenAsync(start, finish);

            return _mapper.Map<IEnumerable<UserEntranceLog>, IEnumerable<UserEntranceLogReadDto>>(logs);
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetEntrancesInAsync(DateTime date)
        {
            var logs = await _unitOfWork.UsersEntrancesLogsRepository.GetEntrancesInAsync(date);

            return _mapper.Map<IEnumerable<UserEntranceLog>, IEnumerable<UserEntranceLogReadDto>>(logs);
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrances(int userId)
        {
            if (userId < 1)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllUserEntrances)}:{nameof(userId)} was less than 1");

                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var userEntrances = await _unitOfWork.UsersEntrancesLogsRepository.GetUserEntrancesAsync(userId);

            return _mapper.Map<IEnumerable<UserEntranceLog>, IEnumerable<UserEntranceLogReadDto>>(userEntrances);
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrances(string userEmail)
        {
            if (userEmail is null)
            {
                _logger.LogError($"{nameof(UsersEntrancesService)}:{nameof(GetAllUserEntrances)}:{nameof(userEmail)} was null");

                throw new ArgumentNullException(nameof(userEmail));
            }

            var userEntrances = await _unitOfWork.UsersEntrancesLogsRepository.GetUserEntrancesAsync(userEmail);

            return _mapper.Map<IEnumerable<UserEntranceLog>, IEnumerable<UserEntranceLogReadDto>>(userEntrances);
        }

        /// <inheritdoc/>
        public async Task<IPaginationResponse<UserEntranceLogReadDto>> GetPaginateableAsync(int pageNumber, int pageSize)
        {
            var items = _mapper.Map<IEnumerable<UserEntranceLog>,
                IEnumerable<UserEntranceLogReadDto>>(await _unitOfWork.UsersEntrancesLogsRepository.GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWork.UsersEntrancesLogsRepository.CountAsync();

            return new PaginationResponse<UserEntranceLogReadDto>(items, totalItemsCount);
        }

        public async Task<bool> LogAsync(int userId)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning($"{nameof(UsersEntrancesService)}: user with id = {userId} not found");

                return false;
            }

            if (user.SendMessageOnEntrance)
                await _emailService.SendEntranceNotificationAsync(user.Email);

            var log = new UserEntranceLog
                      {
                          Entrance = DateTime.Now,
                          User = user,
                          UserId = userId
                      };

            await _unitOfWork.UsersEntrancesLogsRepository.AddAsync(log);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}