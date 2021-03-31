﻿#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [Route("FoundersPCIdentity/Authentication")]
    public class SignUpIdentityController : Controller
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ILogger<SignUpIdentityController> _logger;
        private readonly IRegistrationService _registrationService;

        public SignUpIdentityController(JwtConfiguration jwtConfiguration,
                                        ILogger<SignUpIdentityController> logger,
                                        IRegistrationService registrationService)
        {
            _jwtConfiguration = jwtConfiguration;
            _logger = logger;
            _registrationService = registrationService;
        }

        [Route("SignUp")]
        [HttpPost]
        public async Task<ActionResult<UserSignUpResponse>> Register([FromBody] UserSignUpRequest request)
        {
            if (!ModelState.IsValid) UnprocessableEntity();

            _logger.LogInformation($"{nameof(SignUpIdentityController)}: Registration request with email = {request.Email}");

            var result = await _registrationService.RegisterDefaultUserAsync(request.Email, request.Password);

            if (!result)
            {
                _logger.LogInformation($"{nameof(SignUpIdentityController)}: Registration request with email = {request.Email}. Registration service sent an error");

                return new UserSignUpResponse
                       {
                           Email = request.Email,
                           IsRegistrationSuccessful = false,
                           ResponseException =
                               $"User with email {request.Email} is already exists or may be there another problem. Try another email"
                       };
            }

            _logger.LogInformation($"{nameof(SignUpIdentityController)}: successful registration for user with email = {request.Email}");

            var token = new JwtUserToken(_jwtConfiguration)
                        {
                            Email = request.Email,
                            Role = ApplicationRoles.DefaultUser
                        };

            return new UserSignUpResponse
                   {
                       Email = request.Email,
                       IsRegistrationSuccessful = true,
                       ResponseException = null,
                       Role = ApplicationRoles.DefaultUser,
                       JwtToken = token.GetToken()
                   };
        }
    }
}