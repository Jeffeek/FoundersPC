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

        public async Task<bool> ChangePasswordToAsync(int userId, string newPassword)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null) return false;

            var hashedPassword = _passwordEncryptorService.EncryptPassword(newPassword);

            user.HashedPassword = hashedPassword;

            var resultOfUpdating = await _unitOfWork.UsersRepository.UpdateAsync(user);

            if (!resultOfUpdating) return false;

            var resultOfSaving = await _unitOfWork.SaveChangesAsync();

            return resultOfSaving > 0;
        }

        public async Task<bool> ChangePasswordToAsync(string userEmail, string newPassword)
        {
            var user = await FindUserByEmailAsync(userEmail);

            return await ChangePasswordToAsync(user, newPassword);
        }

        public async Task<bool> ChangePasswordToAsync(UserEntityReadDto user, string newPassword)
        {
            if (ReferenceEquals(user, null)) throw new ArgumentNullException(nameof(user));

            return await ChangePasswordToAsync(user.Id, newPassword);
        }
    }
}
