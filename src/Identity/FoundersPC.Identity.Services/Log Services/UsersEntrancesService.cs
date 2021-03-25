﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.Log_Services
{
    public class UsersEntrancesService : IUsersEntrancesService
    {
        private readonly ILogger<UsersEntrancesService> _logger;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public UsersEntrancesService(IUnitOfWorkUsersIdentity unitOfWork,
                                     IMailService mailService,
                                     ILogger<UsersEntrancesService> logger,
                                     IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
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

        public async Task<bool> LogAsync(int userId)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning($"{nameof(UsersEntrancesService)}: user with id = {userId} not found");

                return false;
            }

            if (user.SendMessageOnEntrance) await _mailService.SendEntranceNotificationAsync(user.Email);

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