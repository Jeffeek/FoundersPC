using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using Microsoft.Extensions.Logging;

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [Route("FoundersPCIdentity/Authentication")]
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

        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<ActionResult<UserForgotPasswordResponse>> ForgotPassword([FromBody] UserForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity();

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
                       IsUserExists = true
                   };
        }
    }
}
