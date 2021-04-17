#region Using namespaces

using System;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Dto;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.User_Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public AuthenticationService(ILogger<AuthenticationService> logger,
                                     IUnitOfWorkUsersIdentity unitOfWork,
                                     PasswordEncryptorService passwordEncryptorService,
                                     IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _passwordEncryptorService = passwordEncryptorService;
            _mapper = mapper;
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="emailOrLogin"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.ArgumentException">Hashed password should be 128 length, but it hasn't.</exception>

        #endregion

        public async Task<UserEntityReadDto> FindUserByEmailOrLoginAndHashedPasswordAsync(string emailOrLogin,
                                                                                          string hashedPassword)
        {
            if (emailOrLogin is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: Find User By Email Or SignIn And Hashed Password: email or login was null");

                throw new ArgumentNullException(nameof(emailOrLogin));
            }

            if (hashedPassword is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: Find User By Email Or SignIn And Hashed Password: hashed password was null");

                throw new ArgumentNullException(nameof(hashedPassword));
            }

            if (hashedPassword.Length != 128)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: Find User By Email Or SignIn And Hashed Password: hashed password was not 128 length");

                throw new ArgumentException($"Hashed password should be 128 length, but was {hashedPassword.Length}");
            }

            var user = await _unitOfWork.UsersRepository.GetUserByAsync(x =>
                                                                            (x.Email == emailOrLogin
                                                                             || x.Login == emailOrLogin)
                                                                            && x.HashedPassword == hashedPassword);

            return _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="emailOrLogin"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.Reflection.TargetInvocationException">
        ///     The algorithm was used with Federal Information
        ///     Processing Standards (FIPS) mode enabled, but is not FIPS compatible.
        /// </exception>
        /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">
        ///     A fallback occurred (for more information, see Character Encoding in .NET)
        ///     -and-
        ///     <see cref="P:System.Text.Encoding.EncoderFallback"/> is set to <see cref="T:System.Text.EncoderExceptionFallback"/>
        ///     .
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">capacity is less than zero.</exception>
        /// <exception cref="T:System.FormatException">
        ///     format includes an unsupported specifier. Supported
        ///     format specifiers are listed in the Remarks section.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">Hashed password should be 128 length, but it hasn't.</exception>

        #endregion

        public Task<UserEntityReadDto> FindUserByEmailOrLoginAndPasswordAsync(string emailOrLogin,
                                                                              string password)
        {
            if (emailOrLogin is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: Find User By Email Or SignIn And Password: email or login was null");

                throw new ArgumentNullException(nameof(emailOrLogin));
            }

            if (password is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: Find User By Email Or SignIn And Password: password was null");

                throw new ArgumentNullException(nameof(password));
            }

            var hashedPassword = _passwordEncryptorService.EncryptPassword(password);

            return FindUserByEmailOrLoginAndHashedPasswordAsync(emailOrLogin, hashedPassword);
        }
    }
}