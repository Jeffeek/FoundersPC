#region Using namespaces

using System;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
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
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public AuthenticationService(ILogger<AuthenticationService> logger,
                                     IMailService mailService,
                                     IUnitOfWorkUsersIdentity unitOfWork,
                                     PasswordEncryptorService passwordEncryptorService,
                                     IMapper mapper)
        {
            _logger = logger;
            _mailService = mailService;
            _unitOfWork = unitOfWork;
            _passwordEncryptorService = passwordEncryptorService;
            _mapper = mapper;
        }

        public async Task<UserEntityReadDto> FindUserByEmailOrLoginAndHashedPasswordAsync(string emailOrLogin, string hashedPassword)
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

        public async Task<UserEntityReadDto> FindUserByEmailOrLoginAndPasswordAsync(string emailOrLogin, string password)
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

            return await FindUserByEmailOrLoginAndHashedPasswordAsync(emailOrLogin, hashedPassword);
        }
    }
}