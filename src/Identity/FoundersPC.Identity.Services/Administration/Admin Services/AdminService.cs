#region Using namespaces

using System;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.Administration.Admin_Services
{
    public class AdminService : IAdminService
    {
        private readonly IApiAccessUsersTokensService _accessUsersTokensService;
        private readonly ILogger<AdminService> _logger;
        private readonly IMailService _mailService;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IUserRegistrationService _registrationService;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;
        private IManagerService _managerService;
        private IMapper _mapper;

        public AdminService(IManagerService managerService,
                            IUserRegistrationService registrationService,
                            IMailService mailService,
                            IMapper mapper,
                            PasswordEncryptorService passwordEncryptorService,
                            IUnitOfWorkUsersIdentity unitOfWork,
                            IApiAccessUsersTokensService accessUsersTokensService,
                            ILogger<AdminService> logger
        )
        {
            _managerService = managerService;
            _registrationService = registrationService;
            _mailService = mailService;
            _mapper = mapper;
            _passwordEncryptorService = passwordEncryptorService;
            _unitOfWork = unitOfWork;
            _accessUsersTokensService = accessUsersTokensService;
            _logger = logger;
        }

        #region New manager registration

        public async Task<bool> RegisterNewManagerAsync(string email, string password)
        {
            if (password is null)
            {
                _logger.LogError($"{nameof(AdminService)}: Input password was null.");

                throw new ArgumentNullException(nameof(password));
            }

            if (email is null)
            {
                _logger.LogError($"{nameof(AdminService)}: Input email was null");

                throw new ArgumentNullException(nameof(email));
            }

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

        #endregion

        #region Block or inactive user

        public async Task<bool> BlockUserAsync(int userId, bool blockAllTokens = true, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null) return false;

            if (!user.IsActive
                || user.IsBlocked)
                return false;

            if (user.Role.RoleTitle == ApplicationRoles.Administrator.ToString()) return false;

            if (blockAllTokens)
            {
                var userTokens = await _unitOfWork.ApiAccessUsersTokensRepository.GetAllUserTokens(userId);
                foreach (var token in userTokens) await _accessUsersTokensService.BlockAsync(token.Id);
            }

            if (sendNotification)
                await _mailService.SendBlockNotificationAsync(user.Email, "You've been blocked, you can be unblocked. Contact the administrator for reasons");

            user.IsBlocked = true;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult) return false;

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> BlockUserAsync(string userEmail, bool blockAllTokens = true, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            if (user is null) return false;

            if (!user.IsActive
                || user.IsBlocked)
                return false;

            if (user.Role.RoleTitle == ApplicationRoles.Administrator.ToString()) return false;

            if (blockAllTokens)
            {
                var userTokens = await _unitOfWork.ApiAccessUsersTokensRepository.GetAllUserTokens(userEmail);
                foreach (var token in userTokens) await _accessUsersTokensService.BlockAsync(token.Id);
            }

            if (sendNotification)
                await _mailService.SendBlockNotificationAsync(user.Email, "You've been blocked, you can be unblocked. Contact the administrator for reasons");

            user.IsBlocked = true;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult) return false;

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> MakeUserInactiveAsync(int userId, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null) return false;

            if (!user.IsActive) return false;

            if (user.Role.RoleTitle == ApplicationRoles.Administrator.ToString()) return false;

            if (sendNotification)
                await _mailService.SendBlockNotificationAsync(user.Email, "You've been blocked, you can't be unblocked.");

            user.IsActive = false;

            return await _unitOfWork.UsersRepository.UpdateAsync(user);
        }

        #endregion

        #region Block API token

        public async Task<bool> BlockAPITokenAsync(int tokenId) => await _accessUsersTokensService.BlockAsync(tokenId);

        public async Task<bool> BlockAPITokenAsync(string token) => await _accessUsersTokensService.BlockAsync(token);

        #endregion
    }
}