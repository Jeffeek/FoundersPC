#region Using namespaces

using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.User_Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly PasswordEncryptorService _encryptorService;
        private readonly ILogger<UserRegistrationService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkUsersIdentity _unitOfWorkUsersIdentity;

        public UserRegistrationService(IUnitOfWorkUsersIdentity unitOfWorkUsersIdentity,
                                       IMapper mapper,
                                       PasswordEncryptorService encryptorService,
                                       ILogger<UserRegistrationService> logger
        )
        {
            _unitOfWorkUsersIdentity = unitOfWorkUsersIdentity;
            _mapper = mapper;
            _encryptorService = encryptorService;
            _logger = logger;
        }

        public async Task<bool> RegisterDefaultUserAsync(string email, string password)
        {
            var defaultUserRole = (await _unitOfWorkUsersIdentity.RolesRepository.GetAllAsync())
                .SingleOrDefault(role => role.RoleTitle == ApplicationRoles.DefaultUser.ToString());

            if (defaultUserRole is null)
            {
                _logger.LogError($"{nameof(UserRegistrationService)}: role 'Default user' not found");

                throw new NoNullAllowedException("No role found");
            }

            _logger.LogInformation($"{nameof(UserRegistrationService)}: role 'Default User' found");

            return await Register(email, password, defaultUserRole);
        }

        public async Task<bool> RegisterManagerAsync(string email, string password)
        {
            var defaultUserRole = (await _unitOfWorkUsersIdentity.RolesRepository.GetAllAsync())
                .SingleOrDefault(role => role.RoleTitle == ApplicationRoles.Manager.ToString());

            if (defaultUserRole is null)
            {
                _logger.LogError($"{nameof(UserRegistrationService)}: role 'Manager' not found");

                throw new NoNullAllowedException("No role found");
            }

            _logger.LogInformation($"{nameof(UserRegistrationService)}: role 'Manager' found");

            return await Register(email, password, defaultUserRole);
        }

        private async Task<bool> Register(string email, string rawPassword, RoleEntity role)
        {
            if (email is null)
            {
                _logger.LogError($"{nameof(UserRegistrationService)}: email was null when tried to register user");

                throw new ArgumentNullException(nameof(email));
            }

            if (rawPassword is null)
            {
                _logger.LogError($"{nameof(UserRegistrationService)}: raw password was null when tried to register user");

                throw new ArgumentNullException(nameof(rawPassword));
            }

            if (role is null)
            {
                _logger.LogError($"{nameof(UserRegistrationService)}: role was null when tried to register user");

                throw new ArgumentNullException(nameof(role));
            }

            var userAlreadyExists = await _unitOfWorkUsersIdentity.UsersRepository
                                                                  .AnyAsync(user => user.Email == email);

            if (userAlreadyExists)
            {
                _logger.LogWarning($"{nameof(UserRegistrationService)}: user with email = {email} is already exist");

                return false;
            }

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