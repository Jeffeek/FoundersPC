#region Using namespaces

using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.User_Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly PasswordEncryptorService _encryptorService;
        private readonly ILogger<RegistrationService> _logger;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWorkUsersIdentity _unitOfWorkUsersIdentity;

        public RegistrationService(IUnitOfWorkUsersIdentity unitOfWorkUsersIdentity,
                                   PasswordEncryptorService encryptorService,
                                   ILogger<RegistrationService> logger,
                                   IEmailService emailService)
        {
            _unitOfWorkUsersIdentity = unitOfWorkUsersIdentity;
            _encryptorService = encryptorService;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<bool> RegisterDefaultUserAsync(string email, string password)
        {
            var defaultUserRole = (await _unitOfWorkUsersIdentity.RolesRepository.GetAllAsync())
                .SingleOrDefault(role => role.RoleTitle == ApplicationRoles.DefaultUser);

            if (defaultUserRole is null)
            {
                _logger.LogError($"{nameof(RegistrationService)}: role 'Default user' not found");

                throw new NoNullAllowedException("No role found");
            }

            _logger.LogInformation($"{nameof(RegistrationService)}: role 'Default User' found");

            return await RegisterAsync(email, password, defaultUserRole);
        }

        public async Task<bool> RegisterManagerAsync(string email, string password)
        {
            var managerRole = (await _unitOfWorkUsersIdentity.RolesRepository.GetAllAsync())
                .SingleOrDefault(role => role.RoleTitle == ApplicationRoles.Manager);

            if (managerRole is null)
            {
                _logger.LogError($"{nameof(RegistrationService)}: role 'Manager' not found");

                throw new NoNullAllowedException("No role found");
            }

            _logger.LogInformation($"{nameof(RegistrationService)}: role 'Manager' found");

            return await RegisterAsync(email, password, managerRole);
        }

        private async Task<bool> RegisterAsync(string email, string rawPassword, RoleEntity role)
        {
            if (email is null)
            {
                _logger.LogError($"{nameof(RegistrationService)}: email was null when tried to register user");

                throw new ArgumentNullException(nameof(email));
            }

            if (rawPassword is null)
            {
                _logger.LogError($"{nameof(RegistrationService)}: raw password was null when tried to register user");

                throw new ArgumentNullException(nameof(rawPassword));
            }

            if (role is null)
            {
                _logger.LogError($"{nameof(RegistrationService)}: role was null when tried to register user");

                throw new ArgumentNullException(nameof(role));
            }

            var userAlreadyExists = await _unitOfWorkUsersIdentity.UsersRepository
                                                                  .AnyAsync(user => user.Email == email);

            if (userAlreadyExists)
            {
                _logger.LogWarning($"{nameof(RegistrationService)}: user with email = {email} is already exist");

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

            await _emailService.SendRegistrationNotificationAsync(email);

            return saveChangesResult > 0;
        }
    }
}