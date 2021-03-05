using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;

namespace FoundersPC.Identity.Services.User_Services
{
    public class DefaultUserService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;
        private readonly  PasswordEncryptorService _passwordEncryptorService;
        private readonly IUsersService _usersService;

        public DefaultUserService(IUnitOfWorkUsersIdentity unitOfWork,
                                  PasswordEncryptorService passwordEncryptorService,
                                  IUsersService usersService
        )
        {
            _unitOfWork = unitOfWork;
            _passwordEncryptorService = passwordEncryptorService;
            _usersService = usersService;
        }

        public async Task<bool> ChangeUserPasswordAsync(string userEmail, string newPassword)
        {
            if (ReferenceEquals(userEmail, null)) throw new ArgumentNullException(nameof(userEmail));
            if (ReferenceEquals(newPassword, null)) throw new ArgumentNullException(nameof(newPassword));
            if (newPassword.Length < 6 || newPassword.Length > 30) throw new ArgumentOutOfRangeException(nameof(newPassword));

            var user = await _usersService.FindUserByEmailAsync(userEmail);

            return await _usersService.ChangePasswordToAsync(user.Id, newPassword);
        }

        public async Task<bool> ChangeUserPasswordAsync(string userEmail, string oldPassword, string newPassword)
        {
            if (ReferenceEquals(userEmail, null)) throw new ArgumentNullException(nameof(userEmail));
            if (ReferenceEquals(oldPassword, null)) throw new ArgumentNullException(nameof(oldPassword));
            if (ReferenceEquals(newPassword, null)) throw new ArgumentNullException(nameof(newPassword));
            if (oldPassword.Length < 6 || oldPassword.Length > 30) throw new ArgumentOutOfRangeException(nameof(oldPassword));
            if (newPassword.Length < 6 || newPassword.Length > 30) throw new ArgumentOutOfRangeException(nameof(newPassword));

            var hashedOldPassword = _passwordEncryptorService.EncryptPassword(oldPassword);

            var user = await _usersService.FindUserByEmailAsync(userEmail);

            var userHashedPassword = user.HashedPassword;

            if (!userHashedPassword.Equals(hashedOldPassword, StringComparison.Ordinal)) return false;

            return await _usersService.ChangePasswordToAsync(user.Id, newPassword);
        }
    }
}
