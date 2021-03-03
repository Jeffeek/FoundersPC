﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;
using Serilog;

namespace FoundersPC.Identity.Services.User_Services
{
    public class AdminService : IAdminService
    {
        private IManagerService _managerService;
        private readonly IUserRegistrationService _registrationService;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;
        private readonly IMailService _mailService;
        private IMapper _mapper;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IApiAccessUsersTokensService _accessUsersTokensService;

        public AdminService(IManagerService managerService,
                            IUserRegistrationService registrationService,
                            IMailService mailService,
                            IMapper mapper,
                            PasswordEncryptorService passwordEncryptorService,
                            IUnitOfWorkUsersIdentity unitOfWork,
                            IApiAccessUsersTokensService accessUsersTokensService
        )
        {
            _managerService = managerService;
            _registrationService = registrationService;
            _mailService = mailService;
            _mapper = mapper;
            _passwordEncryptorService = passwordEncryptorService;
            _unitOfWork = unitOfWork;
            _accessUsersTokensService = accessUsersTokensService;
        }

        public async Task<bool> RegisterNewManagerAsync(string email, string password)
        {
            if (ReferenceEquals(password, null)) throw new ArgumentNullException(nameof(password));
            if (ReferenceEquals(email, null)) throw new ArgumentNullException(nameof(email));

            return await _registrationService.RegisterManagerAsync(email, password);
        }

        public async Task<bool> RegisterNewManagerAsync(string email)
        {
            var random = new Random();
            var newPassword = _passwordEncryptorService.GeneratePassword(random.Next(10, 25));

            var resultOfRegistration = await RegisterNewManagerAsync(email, newPassword);

            var sendResult = await _mailService.SendRegistrationNotificationAsync(email, $"Password for entrance: {newPassword}");

            return resultOfRegistration && sendResult;
        }

        public async Task<bool> BlockUserAsync(int userId, bool blockAllTokens = true, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null) return false;

            if (!user.IsActive || user.IsBlocked) return false;

            if (blockAllTokens)
            {
                var userTokens = user.Tokens;
                foreach (var token in userTokens)
                    await _accessUsersTokensService.BlockAsync(token.Id);
            }

            // todo: add subject
            if (sendNotification)
                await _mailService.SendBlockNotificationAsync(user.Email, "You've been blocked, you can be unblocked. Contact the administrator for reasons");

            user.IsBlocked = true;

            return await _unitOfWork.UsersRepository.UpdateAsync(user);
        }

        public async Task<bool> MakeUserInactiveAsync(int userId, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null) return false;

            if (!user.IsActive) return false;

            // todo: add subject
            if (sendNotification)
                await _mailService.SendBlockNotificationAsync(user.Email, "You've been blocked, you can't be unblocked.");

            user.IsActive = false;

            return await _unitOfWork.UsersRepository.UpdateAsync(user);
        }

        public async Task<bool> BlockAPITokenAsync(int tokenId)
        {
            var token = await _unitOfWork.ApiAccessUsersTokensRepository.GetByIdAsync(tokenId);

            if (token is null) return false;

            if (!token.IsBlocked) return false;

            token.IsBlocked = true;

            return await _unitOfWork.ApiAccessUsersTokensRepository.UpdateAsync(token);
        }

        public async Task<bool> BlockAPITokenAsync(string token)
        {
            var userToken = await _unitOfWork.ApiAccessUsersTokensRepository.GetByTokenAsync(token);

            if (userToken is null) return false;

            if (!userToken.IsBlocked) return false;

            userToken.IsBlocked = true;

            return await _unitOfWork.ApiAccessUsersTokensRepository.UpdateAsync(userToken);
        }
    }
}
