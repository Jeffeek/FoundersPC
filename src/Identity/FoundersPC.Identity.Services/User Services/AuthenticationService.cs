using System;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;

namespace FoundersPC.Identity.Services.User_Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly PasswordEncryptorService _encryptorService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkUsersIdentity _unitOfWorkUsersIdentity;

        public AuthenticationService(IUnitOfWorkUsersIdentity unitOfWorkUsersIdentity,
                                     IMapper mapper,
                                     PasswordEncryptorService encryptorService
        )
        {
            _unitOfWorkUsersIdentity = unitOfWorkUsersIdentity;
            _mapper = mapper;
            _encryptorService = encryptorService;
        }

        public async Task<UserEntityReadDto> FindUserByEmailAsync(string email)
        {
            if (ReferenceEquals(email, null)) return null;

            var user = await _unitOfWorkUsersIdentity.UsersRepository.GetByAsync(x => x.Email == email);

            return user is null ? null : _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }

        public async Task<UserEntityReadDto> FindUserByEmailOrLoginAndHashedPasswordAsync(string emailOrLogin, string hashedPassword)
        {
            if (ReferenceEquals(emailOrLogin, null)) throw new ArgumentNullException(nameof(emailOrLogin));
            if (ReferenceEquals(hashedPassword, null)) throw new ArgumentNullException(nameof(hashedPassword));

            if (hashedPassword.Length != 88) throw new ArgumentException($"Hashed password should be 88 length, but was {hashedPassword.Length}");

            var user = await _unitOfWorkUsersIdentity.UsersRepository.GetByAsync(x =>
                                                                                     (x.Email == emailOrLogin
                                                                                      || x.Login == emailOrLogin)
                                                                                     && x.HashedPassword == hashedPassword);

            return user == null ? null : _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }

        public async Task<UserEntityReadDto> FindUserByEmailOrLoginAndPasswordAsync(string emailOrLogin, string password)
        {
            if (ReferenceEquals(emailOrLogin, null)) throw new ArgumentNullException(nameof(emailOrLogin));
            if (ReferenceEquals(password, null)) throw new ArgumentNullException(nameof(password));

            var hashedPassword = _encryptorService.EncryptPassword(password);

            return await FindUserByEmailOrLoginAndHashedPasswordAsync(emailOrLogin, hashedPassword);
        }

        public async Task<bool> ChangePasswordToAsync(int userId, string newPassword)
        {
            var user = await _unitOfWorkUsersIdentity.UsersRepository.GetByIdAsync(userId);

            var hashedPassword = _encryptorService.EncryptPassword(newPassword);

            user.HashedPassword = hashedPassword;

            var resultOfUpdating = await _unitOfWorkUsersIdentity.UsersRepository.UpdateAsync(user);

            if (!resultOfUpdating) return false;

            var resultOfSaving = await _unitOfWorkUsersIdentity.SaveChangesAsync();

            return resultOfSaving > 0;
        }

        public async Task<bool> ChangePasswordToAsync(UserEntityReadDto user, string newPassword)
        {
            if (ReferenceEquals(user, null)) throw new ArgumentNullException(nameof(user));

            return await ChangePasswordToAsync(user.Id, newPassword);
        }
    }
}
