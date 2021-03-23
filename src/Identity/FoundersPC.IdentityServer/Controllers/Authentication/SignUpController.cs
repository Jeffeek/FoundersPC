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
    public class SignUpController : Controller
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ILogger<SignUpController> _logger;
        private readonly IRegistrationService _registrationService;

        public SignUpController(JwtConfiguration jwtConfiguration,
                                ILogger<SignUpController> logger,
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
            if (!ModelState.IsValid)
                UnprocessableEntity();

            _logger.LogInformation($"{nameof(SignUpController)}: Registration request with email = {request.Email}");

            var result = await _registrationService.RegisterDefaultUserAsync(request.Email, request.Password);

            if (!result)
            {
                _logger.LogInformation($"{nameof(SignUpController)}: Registration request with email = {request.Email}. Registration service sent an error");

                return new UserSignUpResponse
                       {
                           Email = request.Email,
                           IsRegistrationSuccessful = false,
                           ResponseException =
                               $"User with email {request.Email} is already exists or may be there another problem. Try another email"
                       };
            }

            _logger.LogInformation($"{nameof(SignUpController)}: successful registration for user with email = {request.Email}");

            var token = new JwtUserToken(_jwtConfiguration)
                        {
                            Email = request.Email,
                            Role = ApplicationRoles.DefaultUser.ToString()
                        };

            return new UserSignUpResponse
                   {
                       Email = request.Email,
                       IsRegistrationSuccessful = true,
                       ResponseException = null,
                       Role = ApplicationRoles.DefaultUser.ToString(),
                       JwtToken = token.GetToken()
                   };
        }
    }
}
