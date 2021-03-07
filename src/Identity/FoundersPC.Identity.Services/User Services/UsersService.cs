using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;

namespace FoundersPC.Identity.Services.User_Services
{
    //todo: interface
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IMapper _mapper;

        public UsersService(PasswordEncryptorService passwordEncryptorService, IUnitOfWorkUsersIdentity unitOfWork, IMapper mapper)
        {
            _passwordEncryptorService = passwordEncryptorService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserEntityReadDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<UserEntity>, IEnumerable<UserEntityReadDto>>(await _unitOfWork.UsersRepository.GetAllAsync());

        public async Task<UserEntityReadDto> GetByIdAsync(int id) =>
            id < 0 ? null : _mapper.Map<UserEntity, UserEntityReadDto>(await _unitOfWork.UsersRepository.GetByIdAsync(id));

        public async Task<UserEntityReadDto> FindUserByEmailAsync(string email)
        {
            if (ReferenceEquals(email, null)) return null;

            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == email);

            return user is null ? null : _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }

        public async Task<UserEntityReadDto> FindUserByEmailOrLoginAndHashedPasswordAsync(string emailOrLogin, string hashedPassword)
        {
            if (ReferenceEquals(emailOrLogin, null)) throw new ArgumentNullException(nameof(emailOrLogin));
            if (ReferenceEquals(hashedPassword, null)) throw new ArgumentNullException(nameof(hashedPassword));

            if (hashedPassword.Length != 128) throw new ArgumentException($"Hashed password should be 128 length, but was {hashedPassword.Length}");

            var user = await _unitOfWork.UsersRepository.GetByAsync(x =>
                                                                        (x.Email == emailOrLogin
                                                                         || x.Login == emailOrLogin)
                                                                        && x.HashedPassword == hashedPassword);

            return user == null ? null : _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }

        public async Task<UserEntityReadDto> FindUserByEmailOrLoginAndPasswordAsync(string emailOrLogin, string password)
        {
            if (ReferenceEquals(emailOrLogin, null)) throw new ArgumentNullException(nameof(emailOrLogin));
            if (ReferenceEquals(password, null)) throw new ArgumentNullException(nameof(password));

            var hashedPassword = _passwordEncryptorService.EncryptPassword(password);

            return await FindUserByEmailOrLoginAndHashedPasswordAsync(emailOrLogin, hashedPassword);
        }

        public async Task<bool> ChangePasswordToAsync(int userId, string newPassword, string oldHashedPassword)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null) return false;

            if (user.HashedPassword != oldHashedPassword) return false;

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
            if (ReferenceEquals(user, null)) throw new ArgumentNullException(nameof(user));

            return await ChangePasswordToAsync(user.Id, newPassword, oldPassword);
        }

        public async Task<bool> ChangeLoginToAsync(string userEmail, string newLogin)
        {
            if (userEmail is null) throw new ArgumentNullException(nameof(userEmail));
            if (newLogin is null) throw new ArgumentNullException(nameof(newLogin));

            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            if (user is null || user.Login == newLogin) return false;

            user.Login = newLogin;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult) return false;

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangeLoginToAsync(UserEntityReadDto user, string newLogin)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            if (newLogin is null) throw new ArgumentNullException(nameof(newLogin));

            if (user.Email is null) return false;

            return await ChangeLoginToAsync(user.Email, newLogin);
        }

        public async Task<bool> ChangeLoginToAsync(int userId, string newLogin)
        {
            if (userId < 1) throw new ArgumentOutOfRangeException(nameof(userId));
            if (newLogin is null) throw new ArgumentNullException(nameof(newLogin));

            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null || user.Login == newLogin) return false;

            user.Login = newLogin;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult) return false;

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangeNotificationsToAsync(string userEmail, bool notificationOnEntrance, bool notificationOnApiRequest)
        {
            if (userEmail is null) throw new ArgumentNullException(nameof(userEmail));

            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            if (user is null) return false;

            user.SendMessageOnApiRequest = notificationOnApiRequest;
            user.SendMessageOnEntrance = notificationOnEntrance;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult) return false;

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangeNotificationsToAsync(UserEntityReadDto user, bool notificationOnEntrance, bool notificationOnApiRequest)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            if (user.Id < 1) throw new ArgumentOutOfRangeException(nameof(user.Id));

            return await ChangeNotificationsToAsync(user.Id, notificationOnEntrance, notificationOnApiRequest);
        }

        public async Task<bool> ChangeNotificationsToAsync(int userId, bool notificationOnEntrance, bool notificationOnApiRequest)
        {
            if (userId < 1) throw new ArgumentOutOfRangeException(nameof(userId));

            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null) return false;

            user.SendMessageOnApiRequest = notificationOnApiRequest;
            user.SendMessageOnEntrance = notificationOnEntrance;

            var updateResult = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!updateResult) return false;

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
