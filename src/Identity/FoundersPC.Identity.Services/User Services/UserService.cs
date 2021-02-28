#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.Encryption_Services;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;

#endregion

namespace FoundersPC.Identity.Services.User_Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordEncryptorService _encryptorService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkUsersIdentity _unitOfWorkUsersIdentity;

        public UserService(IUnitOfWorkUsersIdentity unitOfWorkUsersIdentity,
                           IPasswordEncryptorService encryptorService,
                           IMapper mapper
        )
        {
            _unitOfWorkUsersIdentity = unitOfWorkUsersIdentity;
            _encryptorService = encryptorService;
            _mapper = mapper;
        }

        public async Task<UserEntityReadDto> GetUserWithEmailAsync(string email)
        {
            if (ReferenceEquals(email, null)) return null;

            var user = await _unitOfWorkUsersIdentity.UsersRepository.GetByAsync(x => x.Email == email);

            return user == null ? null : _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }

        public async Task<UserEntityReadDto> GetUserWithEmailAndPasswordAsync(string emailOrLogin, string rawPassword)
        {
            if (ReferenceEquals(emailOrLogin, null)) return null;
            if (ReferenceEquals(rawPassword, null)) return null;

            var hashedPassword = _encryptorService.EncryptPassword(rawPassword);

            var user = await _unitOfWorkUsersIdentity.UsersRepository.GetByAsync(x =>
                                                                                     (x.Email == emailOrLogin
                                                                                      || x.Login == emailOrLogin)
                                                                                     && x.HashedPassword == hashedPassword);

            return user == null ? null : _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }

        public async Task<bool> RegisterUserAsync(string email, string rawPassword)
        {
            var userAlreadyExists = await _unitOfWorkUsersIdentity.UsersRepository
                                                                  .AnyAsync(user => user.Email == email);

            if (userAlreadyExists) return false;

            var hashedPassword = _encryptorService.EncryptPassword(rawPassword);

            var allRoles = await _unitOfWorkUsersIdentity.RolesRepository.GetAllAsync();

            var defaultUser = allRoles.FirstOrDefault(role => role.RoleTitle == "DefaultUser");

            if (ReferenceEquals(defaultUser, null)) throw new KeyNotFoundException(nameof(defaultUser));

            var newUser = new UserEntity
                          {
                              Email = email,
                              HashedPassword = hashedPassword,
                              IsActive = true,
                              IsBlocked = false,
                              RegistrationDate = DateTime.Now,
                              RoleId = defaultUser.Id,
                              Role = defaultUser
                          };

            await _unitOfWorkUsersIdentity.UsersRepository.AddAsync(newUser);

            var saveChangesResult = await _unitOfWorkUsersIdentity.SaveChangesAsync();

            return saveChangesResult > 0;
        }

        public async Task<bool> RegisterManagerAsync(string email, string rawPassword)
        {
            var userAlreadyExists = await _unitOfWorkUsersIdentity.UsersRepository
                                                                  .AnyAsync(user => user.Email == email);

            if (userAlreadyExists) return false;

            var hashedPassword = _encryptorService.EncryptPassword(rawPassword);

            var allRoles = await _unitOfWorkUsersIdentity.RolesRepository.GetAllAsync();

            var manager = allRoles.FirstOrDefault(role => role.RoleTitle == "Manager");

            if (ReferenceEquals(manager, null)) throw new KeyNotFoundException(nameof(manager));

            var newUser = new UserEntity
                          {
                              Email = email,
                              HashedPassword = hashedPassword,
                              IsActive = true,
                              IsBlocked = false,
                              RegistrationDate = DateTime.Now,
                              Role = manager,
                              RoleId = manager.Id
                          };

            var result = await _unitOfWorkUsersIdentity.UsersRepository.AddAsync(newUser);

            return result != null;
        }

        public async Task<bool> ChangePassword(int id, string newPassword)
        {
            var user = await _unitOfWorkUsersIdentity.UsersRepository.GetByIdAsync(id);

            var hashedPassword = _encryptorService.EncryptPassword(newPassword);

            user.HashedPassword = hashedPassword;

            var resultOfUpdating = await _unitOfWorkUsersIdentity.UsersRepository.UpdateAsync(user);

            if (!resultOfUpdating) return false;

            var resultOfSaving = await _unitOfWorkUsersIdentity.SaveChangesAsync();

            return resultOfSaving > 0;
        }
    }
}