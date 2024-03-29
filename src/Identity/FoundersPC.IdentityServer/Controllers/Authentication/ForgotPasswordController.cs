﻿#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.ApplicationShared.Middleware;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Authentication;
using FoundersPC.RequestResponseShared.IdentityServer.Response.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [AllowAnonymous]
    [Route(IdentityServerRoutes.Authentication.AuthenticationEndpoint)]
    [ModelValidation]
    public class ForgotPasswordController : Controller
    {
        private readonly ILogger<ForgotPasswordController> _logger;
        private readonly IUserSettingsService _userSettingsService;

        public ForgotPasswordController(IUserSettingsService userSettingsService,
                                        ILogger<ForgotPasswordController> logger)
        {
            _userSettingsService = userSettingsService;
            _logger = logger;
        }

        [HttpPost(IdentityServerRoutes.Authentication.ForgotPassword)]
        public async Task<ActionResult<UserForgotPasswordResponse>> ForgotPassword([FromBody] UserForgotPasswordRequest request)
        {
            _logger.LogInformation($"{nameof(ForgotPasswordController)}: Forgot Password request with email = {request.Email}");

            var changePasswordResult =
                await _userSettingsService.GenerateAndChangePasswordToAsync(request.Email);

            if (changePasswordResult)
            {
                _logger.LogError($"{nameof(ForgotPasswordController)}: user with email = {request.Email}: password changed");

                return new UserForgotPasswordResponse
                       {
                           Email = request.Email,
                           IsConfirmationMailSent = true,
                           IsUserExists = true
                       };
            }

            _logger.LogInformation($"{nameof(ForgotPasswordController)}: user with email = {request.Email}: unsuccessful password changing");

            return new UserForgotPasswordResponse
                   {
                       Email = request.Email,
                       Error = "Unsuccessful password changing",
                       IsConfirmationMailSent = false,
                       IsUserExists = false
                   };
        }
    }
}