#region Using namespaces

using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;

#endregion

namespace FoundersPC.Identity.Services.User_Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly PasswordEncryptorService _encryptorService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkUsersIdentity _unitOfWorkUsersIdentity;

        public UserRegistrationService(IUnitOfWorkUsersIdentity unitOfWorkUsersIdentity,
                                       IMapper mapper,
                                       PasswordEncryptorService encryptorService
        )
        {
            _unitOfWorkUsersIdentity = unitOfWorkUsersIdentity;
            _mapper = mapper;
            _encryptorService = encryptorService;
        }

        public async Task<bool> RegisterDefaultUserAsync(string email, string password)
        {
            var defaultUserRole = (await _unitOfWorkUsersIdentity.RolesRepository.GetAllAsync())
                .SingleOrDefault(role => role.RoleTitle == "DefaultUser");

            if (ReferenceEquals(defaultUserRole, null)) throw new NoNullAllowedException("No role found");

            return await Register(email, password, defaultUserRole);
        }

        public async Task<bool> RegisterManagerAsync(string email, string password)
        {
            var defaultUserRole = (await _unitOfWorkUsersIdentity.RolesRepository.GetAllAsync())
                .SingleOrDefault(role => role.RoleTitle == "Manager");

            if (ReferenceEquals(defaultUserRole, null)) throw new NoNullAllowedException("No role found");

            return await Register(email, password, defaultUserRole);
        }

        private async Task<bool> Register(string email, string rawPassword, RoleEntity role)
        {
            if (ReferenceEquals(email, null)) throw new ArgumentNullException(nameof(email));
            if (ReferenceEquals(rawPassword, null)) throw new ArgumentNullException(nameof(rawPassword));
            if (ReferenceEquals(role, null)) throw new ArgumentNullException(nameof(role));

            var userAlreadyExists = await _unitOfWorkUsersIdentity.UsersRepository
                                                                  .AnyAsync(user => user.Email == email);

            if (userAlreadyExists) return false;

            var hashedPassword = _encryptorService.EncryptPassword(rawPassword);

            var newUser = new UserEntity
                          {
                              Email = email,
                              HashedPassword = hashedPassword,
                              IsActive = true,
                              IsBlocked = false,
                              RegistrationDate = DateTime.Now,
                              RoleId = role.Id,
                              Role = role
                          };

            await _unitOfWorkUsersIdentity.UsersRepository.AddAsync(newUser);

            var saveChangesResult = await _unitOfWorkUsersIdentity.SaveChangesAsync();

            return saveChangesResult > 0;
        }
    }
}