﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Infrastructure.UnitOfWork;

#endregion

namespace FoundersPC.Identity.Services.Log_Services
{
    public class UsersEntrancesService : IUsersEntrancesService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;
        private readonly IMailService _mailService;

        public UsersEntrancesService(IUnitOfWorkUsersIdentity unitOfWork, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }

        public async Task<IEnumerable<UserEntranceLog>> GetAllAsync() =>
            await _unitOfWork.UsersEntrancesLogsRepository.GetAllAsync();

        public async Task<UserEntranceLog> GetByIdAsync(int id) =>
            await _unitOfWork.UsersEntrancesLogsRepository.GetByIdAsync(id);

        public async Task<IEnumerable<UserEntranceLog>> GetEntrancesBetweenAsync(DateTime start, DateTime finish)
        {
            var allLogs = await _unitOfWork.UsersEntrancesLogsRepository.GetAllAsync();

            var filtered = allLogs
                .Where(log => log.Entrance >= start && log.Entrance <= finish);

            return filtered;
        }

        public async Task<IEnumerable<UserEntranceLog>> GetEntrancesInAsync(DateTime date)
        {
            var allLogs = await _unitOfWork.UsersEntrancesLogsRepository.GetAllAsync();

            var filtered = allLogs
                .Where(log => log.Entrance.Year == date.Year
                              && log.Entrance.Month == date.Month
                              && log.Entrance.Day == date.Day);

            return filtered;
        }

        //todo: logger
        public async Task<bool> LogAsync(int userId)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user == null) return false;

            if (user.SendMessageOnEntrance)
                await _mailService.SendEntranceNotificationAsync(user.Email);

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