#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Dto;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.Log_Services
{
    public class UsersEntrancesService : IUsersEntrancesService
    {
        private readonly ILogger<UsersEntrancesService> _logger;
        private readonly IEmailService _emailService;
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
                .UsersEntrancesLogsRepository.GetAllAsync());

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
            // todo: logger
            if (userId < 1) throw new ArgumentOutOfRangeException(nameof(userId));

            var userEntrances = await _unitOfWork.UsersEntrancesLogsRepository.GetUserEntrancesAsync(userId);

            return _mapper.Map<IEnumerable<UserEntranceLog>, IEnumerable<UserEntranceLogReadDto>>(userEntrances);
        }

        public async Task<IEnumerable<UserEntranceLogReadDto>> GetAllUserEntrances(string userEmail)
        {
            // todo: logger
            if (userEmail is null) throw new ArgumentNullException(nameof(userEmail));

            var userEntrances = await _unitOfWork.UsersEntrancesLogsRepository.GetUserEntrancesAsync(userEmail);

            return _mapper.Map<IEnumerable<UserEntranceLog>, IEnumerable<UserEntranceLogReadDto>>(userEntrances);
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