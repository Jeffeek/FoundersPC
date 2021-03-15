#region Using namespaces

using System;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Services.Encryption_Services;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [Route("identityAPI/authentication")]
    [ApiController]
    public class AuthenticationAndRegistrationController : Controller
    {
        private readonly ILogger<AuthenticationAndRegistrationController> _logger;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUsersEntrancesService _usersEntrancesService;
        private readonly IUsersInformationService _usersInformationService;

        public AuthenticationAndRegistrationController(IUsersInformationService authenticationInformationService,
                                                       IMailService mailService,
                                                       PasswordEncryptorService passwordEncryptorService,
                                                       IUserRegistrationService userRegistrationService,
                                                       IUsersEntrancesService usersEntrancesService,
                                                       IMapper mapper,
                                                       ILogger<AuthenticationAndRegistrationController> logger
        )
        {
            _usersInformationService = authenticationInformationService;
            _mailService = mailService;
            _passwordEncryptorService = passwordEncryptorService;
            _userRegistrationService = userRegistrationService;
            _usersEntrancesService = usersEntrancesService;
            _mapper = mapper;
            _logger = logger;
        }

        [Route("forgotpassword")]
        [HttpPost]
        public async Task<ActionResult<UserForgotPasswordResponse>> ForgotPassword(UserForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new UserForgotPasswordResponse
                                  {
                                      Error = "Bad model",
                                      Email = request.Email,
                                      IsConfirmationMailSent = false,
                                      IsUserExists = false
                                  });

            _logger.LogInformation($"{nameof(AuthenticationAndRegistrationController)}: Forgot Password request with email = {request.Email}");

            var user = await _usersInformationService.FindUserByEmailAsync(request.Email);

            if (user is null)
            {
                _logger.LogWarning($"{nameof(AuthenticationAndRegistrationController)}: user with email = {request.Email} is not found");

                return new UserForgotPasswordResponse
                       {
                           Email = request.Email,
                           Error = "User with this email doesn't exists",
                           IsConfirmationMailSent = false,
                           IsUserExists = false
                       };
            }

            var newPassword = _passwordEncryptorService.GeneratePassword();
            var updateResult = await _usersInformationService.ChangePasswordToAsync(user.Id, newPassword, user.HashedPassword);

            if (updateResult == false)
            {
                _logger.LogError($"{nameof(AuthenticationAndRegistrationController)}: change password db update error");

                return new UserForgotPasswordResponse
                       {
                           Email = user.Email,
                           Error = "Error happened when application tried to update the database",
                           IsUserExists = true,
                           IsConfirmationMailSent = false
                       };
            }

            var emailSendResult = await _mailService.SendNewPasswordAsync(user.Email, newPassword);

            if (emailSendResult)
            {
                _logger.LogError($"{nameof(AuthenticationAndRegistrationController)}: user with email = {request.Email}: password changed, but email message not sent");

                return new UserForgotPasswordResponse
                       {
                           Email = user.Email,
                           IsConfirmationMailSent = false,
                           IsUserExists = true,
                           Error =
                               $"New password: {newPassword}{Environment.NewLine}The password was changed. But our Email Daemon didn't send a new password to you."
                       };
            }

            _logger.LogInformation($"{nameof(AuthenticationAndRegistrationController)}: user with email = {request.Email}: password changed and email message sent");

            return new UserForgotPasswordResponse
                   {
                       Email = user.Email,
                       Error = null,
                       IsConfirmationMailSent = true,
                       IsUserExists = true
                   };
        }

        [Route("registration")]
        [HttpPost]
        public async Task<ActionResult<UserRegisterResponse>> Register(UserSignUpRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new UserRegisterResponse
                                  {
                                      Email = request.Email,
                                      IsRegistrationSuccessful = false,
                                      ResponseException = "Bad model",
                                      Role = null,
                                      UserId = -1
                                  });

            _logger.LogInformation($"{nameof(AuthenticationAndRegistrationController)}: Registration request with email = {request.Email}");

            var result = await _userRegistrationService.RegisterDefaultUserAsync(request.Email, request.Password);

            if (!result)
            {
                _logger.LogInformation($"{nameof(AuthenticationAndRegistrationController)}: Registration request with email = {request.Email}. Registration service sent an error");

                return new UserRegisterResponse
                       {
                           Email = request.Email,
                           IsRegistrationSuccessful = false,
                           ResponseException = $"User with email {request.Email} is already exists or may be there another problem. Try another email"
                       };
            }

            var user = await _usersInformationService.FindUserByEmailAsync(request.Email);

            _logger.LogInformation($"{nameof(AuthenticationAndRegistrationController)}: successful registration for user with email = {request.Email}");

            return new UserRegisterResponse
                   {
                       Email = request.Email,
                       IsRegistrationSuccessful = true,
                       ResponseException = null,
                       Role = "DefaultUser",
                       UserId = user.Id
                   };
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<UserLoginResponse>> Login(UserSignInRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new UserLoginResponse
                                  {
                                      MetaInfo = "Bad model"
                                  });

            var user = await _usersInformationService.FindUserByEmailOrLoginAndPasswordAsync(request.LoginOrEmail, request.Password);

            if (user is null)
            {
                _logger.LogWarning($"{nameof(AuthenticationAndRegistrationController)}: user with email or login = {request.LoginOrEmail} and wrote password not exist. Try another password.");

                return NotFound(new UserLoginResponse());
            }

            _logger.LogInformation($"{nameof(AuthenticationAndRegistrationController)}: user with email or login = {request.LoginOrEmail} entered the service");
            await _usersEntrancesService.LogAsync(user.Id);
            if (user.SendMessageOnEntrance) await _mailService.SendEntranceNotificationAsync(user.Email);

            var mappedUser = _mapper.Map<UserEntityReadDto, UserLoginResponse>(user);
            mappedUser.IsUserExists = true;

            return mappedUser;
        }
    }
}