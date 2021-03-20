#region Using namespaces

using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
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

        #region Make user inactive

        public async Task<bool> MakeUserInactiveAsync(int userId, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            return await MakeUserInactiveAsync(user, sendNotification);
        }

        public async Task<bool> MakeUserInactiveAsync(string email, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == email);

            return await MakeUserInactiveAsync(user, sendNotification);
        }

        private async Task<bool> MakeUserInactiveAsync(UserEntity user, bool sendNotification)
        {
            if (user is null) return false;

            if (!user.IsActive) return false;

            if (user.Role.RoleTitle == ApplicationRoles.Administrator.ToString()) return false;

            if (sendNotification) await _mailService.SendBlockNotificationAsync(user.Email, "You've been blocked, you can't be unblocked.");

            user.IsActive = false;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (updateResult) return await _unitOfWork.SaveChangesAsync() > 0;

            return false;
        }

        #endregion

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

            var registrationResult = await _registrationService.RegisterManagerAsync(email, password);

            if (registrationResult) return await _mailService.SendRegistrationNotificationAsync(email, $"Password for entrance: {password}");

            return false;
        }

        public async Task<bool> RegisterNewManagerAsync(string email)
        {
            var random = new Random();
            var newPassword = _passwordEncryptorService.GeneratePassword(random.Next(10, 25));

            return await RegisterNewManagerAsync(email, newPassword);
        }

        #endregion

        #region Block / Unblock user

        #region Block

        public async Task<bool> BlockUserAsync(int userId, bool blockAllTokens = true, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            var changeStatusResult = await ChangeUserBlockStatusAsync(user, true, sendNotification);

            if (!changeStatusResult) return false;

            if (!blockAllTokens) return true;

            return await BlockAllUserTokensAsync(user.Id);
        }

        public async Task<bool> BlockUserAsync(string userEmail, bool blockAllTokens = true, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            if (user is null) return false;

            return await BlockUserAsync(user.Id);
        }

        private async Task<bool> BlockAllUserTokensAsync(int userId)
        {
            var userTokens = await _unitOfWork.ApiAccessUsersTokensRepository.GetAllUserTokens(userId);
            foreach (var token in userTokens.Where(token => !token.IsBlocked && token.ExpirationDate >= DateTime.Now))
                await _accessUsersTokensService.BlockAsync(token.Id);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        #endregion

        #region Unblock

        public async Task<bool> UnBlockUserAsync(int userId, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            var changeStatusResult = await ChangeUserBlockStatusAsync(user, false, sendNotification);

            return changeStatusResult;
        }

        public async Task<bool> UnBlockUserAsync(string userEmail, bool sendNotification = true)
        {
            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            var changeStatusResult = await ChangeUserBlockStatusAsync(user, false, sendNotification);

            return changeStatusResult;
        }

        #endregion

        /// <summary>
        ///     Blocks/Unblocks user
        /// </summary>
        /// <param name="user">User to change</param>
        /// <param name="block">true - blocking, false - unblocking</param>
        /// <param name="sendNotification">Send notification to user via email</param>
        /// <returns></returns>
        private async Task<bool> ChangeUserBlockStatusAsync(UserEntity user, bool block, bool sendNotification)
        {
            if (user is null) return false;

            if (!user.IsActive) return false;

            if (user.Role.RoleTitle == ApplicationRoles.Administrator.ToString()) return false;

            user.IsBlocked = block;

            if (sendNotification)
            {
                if (block)
                    await _mailService.SendBlockNotificationAsync(user.Email,
                                                                  "You've been BLOCKED, you can be unblocked. Contact the administrator for reasons");
                else
                    await _mailService.SendUnBlockNotificationAsync(user.Email, "You've been UNBLOCKED.");
            }

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult) return false;

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        #endregion

        #region Block API token

        public async Task<bool> BlockAPITokenAsync(int tokenId) => await _accessUsersTokensService.BlockAsync(tokenId);

        public async Task<bool> BlockAPITokenAsync(string token) => await _accessUsersTokensService.BlockAsync(token);

        #endregion
    }
}