﻿#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.AuthenticationShared.Request;
using FoundersPC.AuthenticationShared.Response;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Services.Encryption_Services;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [Route("authAPI/forgotpassword")]
    [ApiController]
    public class ForgotPasswordController : Controller
    {
        private readonly IMailService _mailService;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IAuthenticationService _authenticationService;

        public ForgotPasswordController(IAuthenticationService authenticationService,
                                        IMailService mailService,
                                        PasswordEncryptorService passwordEncryptorService
        )
        {
            _authenticationService = authenticationService;
            _mailService = mailService;
            _passwordEncryptorService = passwordEncryptorService;
        }

        [HttpPost]
        public async Task<UserForgotPasswordResponse> ForgotPassword(UserForgotPasswordRequest request)
        {
            if (ModelState.IsValid == false)
                return new UserForgotPasswordResponse
                       {
                           EmailSendError = "Bad model",
                           Email = request.Email,
                           IsConfirmationMailSent = false,
                           IsUserExists = false
                       };

            var user = await _authenticationService.FindUserByEmailAsync(request.Email);

            if (user == null)
                return new UserForgotPasswordResponse
                       {
                           Email = request.Email,
                           EmailSendError = "User with this email doesn't exists",
                           IsConfirmationMailSent = false,
                           IsUserExists = false
                       };

            var newPassword = _passwordEncryptorService.GeneratePassword();
            var updateResult = await _authenticationService.ChangePasswordToAsync(user.Id, newPassword);

            if (updateResult == false)
                return new UserForgotPasswordResponse
                       {
                           Email = user.Email,
                           EmailSendError = "Error happened when application tried to update the database",
                           IsUserExists = true,
                           IsConfirmationMailSent = false
                       };

            var emailSendResult = await _mailService.SendNewPasswordAsync(user.Email, newPassword);

            return emailSendResult is true
                       ? new UserForgotPasswordResponse
                         {
                             Email = user.Email,
                             EmailSendError = null,
                             IsConfirmationMailSent = true,
                             IsUserExists = true
                         }
                       : new UserForgotPasswordResponse
                         {
                             Email = user.Email,
                             IsConfirmationMailSent = false,
                             IsUserExists = true,
                             EmailSendError =
                                 $"New password: {newPassword}\nThe password was changed. But our Email Daemon didn't send a new password to you."
                         };
        }
    }
}