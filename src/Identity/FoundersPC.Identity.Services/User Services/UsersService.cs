#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.User_Services
{
    public class UsersService : IUsersService
    {
        private readonly ILogger<UsersService> _logger;
        private readonly IMapper _mapper;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public UsersService(PasswordEncryptorService passwordEncryptorService,
                            IUnitOfWorkUsersIdentity unitOfWork,
                            IMapper mapper,
                            ILogger<UsersService> logger
        )
        {
            _passwordEncryptorService = passwordEncryptorService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserEntityReadDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<UserEntity>, IEnumerable<UserEntityReadDto>>(await _unitOfWork.UsersRepository.GetAllAsync());

        public async Task<UserEntityReadDto> GetByIdAsync(int id) =>
            id < 0 ? null : _mapper.Map<UserEntity, UserEntityReadDto>(await _unitOfWork.UsersRepository.GetByIdAsync(id));

        public async Task<UserEntityReadDto> FindUserByEmailAsync(string email)
        {
            if (email is null)
            {
                _logger.LogError($"{nameof(UsersService)}: user by email: email was null");

                throw new ArgumentNullException(nameof(email));
            }

            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == email);

            return user is null ? null : _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }

        public async Task<UserEntityReadDto> FindUserByEmailOrLoginAndHashedPasswordAsync(string emailOrLogin, string hashedPassword)
        {
            if (emailOrLogin is null)
            {
                _logger.LogError($"{nameof(UsersService)}: Find User By Email Or Login And Hashed Password: email or login was null");

                throw new ArgumentNullException(nameof(emailOrLogin));
            }

            if (hashedPassword is null)
            {
                _logger.LogError($"{nameof(UsersService)}: Find User By Email Or Login And Hashed Password: hashed password was null");

                throw new ArgumentNullException(nameof(hashedPassword));
            }

            if (hashedPassword.Length != 128)
            {
                _logger.LogError($"{nameof(UsersService)}: Find User By Email Or Login And Hashed Password: hashed password was not 128 length");

                throw new ArgumentException($"Hashed password should be 128 length, but was {hashedPassword.Length}");
            }

            var user = await _unitOfWork.UsersRepository.GetByAsync(x =>
                                                                        (x.Email == emailOrLogin
                                                                         || x.Login == emailOrLogin)
                                                                        && x.HashedPassword == hashedPassword);

            return user == null ? null : _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }

        public async Task<UserEntityReadDto> FindUserByEmailOrLoginAndPasswordAsync(string emailOrLogin, string password)
        {
            if (emailOrLogin is null)
            {
                _logger.LogError($"{nameof(UsersService)}: Find User By Email Or Login And Password: email or login was null");

                throw new ArgumentNullException(nameof(emailOrLogin));
            }

            if (password is null)
            {
                _logger.LogError($"{nameof(UsersService)}: Find User By Email Or Login And Password: password was null");

                throw new ArgumentNullException(nameof(password));
            }

            var hashedPassword = _passwordEncryptorService.EncryptPassword(password);

            return await FindUserByEmailOrLoginAndHashedPasswordAsync(emailOrLogin, hashedPassword);
        }

        public async Task<bool> ChangePasswordToAsync(int userId, string newPassword, string oldHashedPassword)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null)
            {
                _logger.LogError($"{nameof(UsersService)}: change password: user model was null");

                return false;
            }

            if (user.HashedPassword != oldHashedPassword)
            {
                _logger.LogError($"{nameof(UsersService)}: change password: user's hashed password was not equal to old hashed password");

                return false;
            }

            var hashedNewPassword = _passwordEncryptorService.EncryptPassword(newPassword);

            user.HashedPassword = hashedNewPassword;

            var resultOfUpdating = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!resultOfUpdating) return false;

            var resultOfSaving = await _unitOfWork.SaveChangesAsync();

            return resultOfSaving > 0;
        }

        public async Task<bool> ChangePasswordToAsync(string userEmail, string newPassword, string oldHashedPassword)
        {
            var user = await FindUserByEmailAsync(userEmail);

            return await ChangePasswordToAsync(user, newPassword, oldHashedPassword);
        }

        public async Task<bool> ChangePasswordToAsync(UserEntityReadDto user, string newPassword, string oldPassword)
        {
            if (user is not null) return await ChangePasswordToAsync(user.Id, newPassword, oldPassword);

            _logger.LogError($"{nameof(UsersService)}: change password: user model was null");

            throw new ArgumentNullException(nameof(user));
        }

        public async Task<bool> ChangeLoginToAsync(string userEmail, string newLogin)
        {
            if (userEmail is null)
            {
                _logger.LogError($"{nameof(UsersService)}: change login: email was null");

                throw new ArgumentNullException(nameof(userEmail));
            }

            if (newLogin is null)
            {
                _logger.LogError($"{nameof(UsersService)}: change login: new login was null");

                throw new ArgumentNullException(nameof(newLogin));
            }

            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            if (user is null
                || user.Login == newLogin)
            {
                _logger.LogError(user is null
                                     ? $"{nameof(UsersService)}: change login: user from db was null"
                                     : $"{nameof(UsersService)}: change login: user's login was equal to new login");

                return false;
            }

            user.Login = newLogin;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (updateResult) return await _unitOfWork.SaveChangesAsync() > 0;

            _logger.LogError($"{nameof(UsersService)}: change login: update result was false");

            return false;
        }

        public async Task<bool> ChangeLoginToAsync(UserEntityReadDto user, string newLogin)
        {
            if (user is null)
            {
                _logger.LogError($"{nameof(UsersService)}: change login: user model was null");

                throw new ArgumentNullException(nameof(user));
            }

            if (newLogin is null)
            {
                _logger.LogError($"{nameof(UsersService)}: change login: new login was null");

                throw new ArgumentNullException(nameof(newLogin));
            }

            if (user.Email is not null) return await ChangeLoginToAsync(user.Email, newLogin);

            _logger.LogError($"{nameof(UsersService)}: change login: user email was null");

            return false;
        }

        public async Task<bool> ChangeLoginToAsync(int userId, string newLogin)
        {
            if (userId < 1)
            {
                _logger.LogError($"{nameof(UsersService)}: change login: user id < 1");

                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            if (newLogin is null)
            {
                _logger.LogError($"{nameof(UsersService)}: change login: new login was null");

                throw new ArgumentNullException(nameof(newLogin));
            }

            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null
                || user.Login == newLogin)
            {
                _logger.LogError(user is null
                                     ? $"{nameof(UsersService)}: change login: user from db was null"
                                     : $"{nameof(UsersService)}: change login: user's login was equal to new login");

                return false;
            }

            user.Login = newLogin;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (updateResult) return await _unitOfWork.SaveChangesAsync() > 0;

            _logger.LogError($"{nameof(UsersService)}: change login: user from db was null");

            return false;
        }

        public async Task<bool> ChangeNotificationsToAsync(string userEmail, bool notificationOnEntrance, bool notificationOnApiRequest)
        {
            if (userEmail is null)
            {
                _logger.LogError($"{nameof(UsersService)}: change notifications: user email was null");

                throw new ArgumentNullException(nameof(userEmail));
            }

            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            if (user is null)
            {
                _logger.LogError($"{nameof(UsersService)}: change notifications: user from db was null");

                return false;
            }

            user.SendMessageOnApiRequest = notificationOnApiRequest;
            user.SendMessageOnEntrance = notificationOnEntrance;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (updateResult) return await _unitOfWork.SaveChangesAsync() > 0;

            _logger.LogError($"{nameof(UsersService)}: change notifications: update result was null");

            return false;
        }

        public async Task<bool> ChangeNotificationsToAsync(UserEntityReadDto user, bool notificationOnEntrance, bool notificationOnApiRequest)
        {
            if (user is null)
            {
                _logger.LogError($"{nameof(UsersService)}: change notifications: user model was null");

                throw new ArgumentNullException(nameof(user));
            }

            if (user.Id >= 1) return await ChangeNotificationsToAsync(user.Id, notificationOnEntrance, notificationOnApiRequest);

            _logger.LogError($"{nameof(UsersService)}: change notifications: user id < 1");

            throw new ArgumentOutOfRangeException(nameof(user.Id));
        }

        public async Task<bool> ChangeNotificationsToAsync(int userId, bool notificationOnEntrance, bool notificationOnApiRequest)
        {
            if (userId < 1)
            {
                _logger.LogError($"{nameof(UsersService)}: change notifications: user id < 1");

                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null)
            {
                _logger.LogError($"{nameof(UsersService)}: change notifications: user from db was null");

                return false;
            }

            user.SendMessageOnApiRequest = notificationOnApiRequest;
            user.SendMessageOnEntrance = notificationOnEntrance;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult)
            {
                _logger.LogError($"{nameof(UsersService)}: change notifications: update result was null");

                return false;
            }

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}