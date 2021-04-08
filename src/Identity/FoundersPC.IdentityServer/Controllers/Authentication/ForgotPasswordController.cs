#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [AllowAnonymous]
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

        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<UserForgotPasswordResponse>> ForgotPassword(
            [FromBody] UserForgotPasswordRequest request)
        {
            if (!ModelState.IsValid) return UnprocessableEntity();

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