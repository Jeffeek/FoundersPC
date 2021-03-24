#region Using namespaces

using System;
using System.Data;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.User_Services.Settings
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly ILogger<UserSettingsService> _logger;
        private readonly IMailService _mailService;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public UserSettingsService(IUnitOfWorkUsersIdentity unitOfWork,
                                   ILogger<UserSettingsService> logger,
                                   PasswordEncryptorService passwordEncryptorService,
                                   IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _passwordEncryptorService = passwordEncryptorService;
            _mailService = mailService;
        }

        #region Change password

        #region Private part

        // todo: logger
        private async Task<bool> ChangePasswordToAsync(UserEntity user, string oldPassword, string newPassword)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            if (newPassword is null) throw new ArgumentNullException(nameof(newPassword));
            if (oldPassword is null) throw new ArgumentNullException(nameof(oldPassword));

            var oldHashedPassword = _passwordEncryptorService.EncryptPassword(oldPassword);

            if (!oldHashedPassword.Equals(user.HashedPassword, StringComparison.Ordinal))
                throw new
                    ArgumentException($"Old password in hash is not equal to database's hashed password for user with id = {user.Id}",
                                      nameof(oldPassword));

            return await ChangePasswordToAsync(user, newPassword);
        }

        private async Task<bool> ChangePasswordToAsync(UserEntity user, string newPassword)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            if (newPassword is null) throw new ArgumentNullException(nameof(newPassword));

            var hashedNewPassword = _passwordEncryptorService.EncryptPassword(newPassword);

            user.HashedPassword = hashedNewPassword;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult) return false;

            await _mailService.SendNewPasswordAsync(user.Email, newPassword);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        #endregion

        public async Task<bool> ChangePasswordToAsync(int userId, string oldPassword, string newPassword)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            return await ChangePasswordToAsync(user, oldPassword, newPassword);
        }

        public async Task<bool> ChangePasswordToAsync(string userEmail, string newPassword, string oldPassword)
        {
            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            return await ChangePasswordToAsync(user, oldPassword, newPassword);
        }

        #region Generate and change

        public async Task<bool> GenerateAndChangePasswordToAsync(int userId)
        {
            if (userId < 1) throw new ArgumentOutOfRangeException(nameof(userId));

            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null) throw new NoNullAllowedException($"user with id = {userId} is not found");

            var newPassword = _passwordEncryptorService.GeneratePassword(10);

            var hashedNewPassword = _passwordEncryptorService.EncryptPassword(newPassword);

            user.HashedPassword = hashedNewPassword;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (updateResult) return await _unitOfWork.SaveChangesAsync() > 0;

            return false;
        }

        public async Task<bool> GenerateAndChangePasswordToAsync(string userEmail)
        {
            if (userEmail is null) throw new ArgumentNullException(nameof(userEmail));

            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            if (user is null) throw new NoNullAllowedException($"user with email = {userEmail} is not found");

            var newPassword = _passwordEncryptorService.GeneratePassword(10);

            var hashedNewPassword = _passwordEncryptorService.EncryptPassword(newPassword);

            user.HashedPassword = hashedNewPassword;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (updateResult) return await _unitOfWork.SaveChangesAsync() > 0;

            return false;
        }

        #endregion

        #endregion

        #region Change login

        private async Task<bool> ChangeLoginToAsync(UserEntity user, string newLogin)
        {
            if (user is null
                || user.Login == newLogin)
            {
                _logger.LogError(user is null
                                     ? $"{nameof(UsersInformationService)}: change login: user from db was null"
                                     : $"{nameof(UsersInformationService)}: change login: user's login was equal to new login");

                return false;
            }

            var isAnyOneHasSameLogin = await _unitOfWork.UsersRepository.AnyAsync(x => x.Login == newLogin);

            if (isAnyOneHasSameLogin)
            {
                _logger.LogWarning($"User with email = {user.Email} (id = {user.Id}) tried to change login to {newLogin}, but it's using by someone else");

                return false;
            }

            user.Login = newLogin;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (updateResult) return await _unitOfWork.SaveChangesAsync() > 0;

            _logger.LogError($"{nameof(UsersInformationService)}: change login: update result was unsuccessful");

            return false;
        }

        public async Task<bool> ChangeLoginToAsync(string userEmail, string newLogin)
        {
            if (userEmail is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: change login: email was null");

                throw new ArgumentNullException(nameof(userEmail));
            }

            if (newLogin is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: change login: new login was null");

                throw new ArgumentNullException(nameof(newLogin));
            }

            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            return await ChangeLoginToAsync(user, newLogin);
        }

        public async Task<bool> ChangeLoginToAsync(int userId, string newLogin)
        {
            if (userId < 1)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: change login: user id < 1");

                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            if (newLogin is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: change login: new login was null");

                throw new ArgumentNullException(nameof(newLogin));
            }

            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            return await ChangeLoginToAsync(user, newLogin);
        }

        #endregion

        // todo: change boolean notification settings to an object

        #region Change notifications

        public async Task<bool> ChangeNotificationsToAsync(string userEmail,
                                                           bool notificationOnEntrance,
                                                           bool notificationOnApiRequest)
        {
            if (userEmail is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: change notifications: user email was null");

                throw new ArgumentNullException(nameof(userEmail));
            }

            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            if (user is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: change notifications: user from db was null");

                return false;
            }

            user.SendMessageOnApiRequest = notificationOnApiRequest;
            user.SendMessageOnEntrance = notificationOnEntrance;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (updateResult) return await _unitOfWork.SaveChangesAsync() > 0;

            _logger.LogError($"{nameof(UsersInformationService)}: change notifications: update result was null");

            return false;
        }

        public async Task<bool> ChangeNotificationsToAsync(int userId,
                                                           bool notificationOnEntrance,
                                                           bool notificationOnApiRequest)
        {
            if (userId < 1)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: change notifications: user id < 1");

                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: change notifications: user from db was null");

                return false;
            }

            user.SendMessageOnApiRequest = notificationOnApiRequest;
            user.SendMessageOnEntrance = notificationOnEntrance;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: change notifications: update result was null");

                return false;
            }

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        #endregion
    }
}