#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.ApplicationShared.Jwt;
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

        #region Docs

        /// <exception cref="T:Microsoft.IdentityModel.Tokens.SecurityTokenEncryptionFailedException">
        ///     both
        ///     <see cref="P:System.IdentityModel.Tokens.Jwt.JwtSecurityToken.SigningCredentials"/> and
        ///     <see cref="P:System.IdentityModel.Tokens.Jwt.JwtSecurityToken.InnerToken"/> are set.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">Key is <see langword="null"/>.</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">
        ///     A fallback occurred (for more information, see Character Encoding in .NET)
        ///     -and-
        ///     <see cref="P:System.Text.Encoding.EncoderFallback"/> is set to <see cref="T:System.Text.EncoderExceptionFallback"/>
        ///     .
        /// </exception>
        /// <exception cref="T:System.ArgumentException">If 'expires' &lt;= 'notbefore'.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     The resulting <see cref="T:System.DateTime"/> is less than
        ///     <see cref="F:System.DateTime.MinValue"/> or greater than <see cref="F:System.DateTime.MaxValue"/>.
        /// </exception>

        #endregion

        [HttpPost(IdentityServerRoutes.Authentication.SignUp)]
        public async Task<ActionResult<UserSignUpResponse>> SignUp([FromBody] UserSignUpRequest request)
        {
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

        [HttpPost]
        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [Route(IdentityServerRoutes.Authentication.SignUpManager)]
        public async Task<ActionResult<UserSignUpResponse>> SignUpManager([FromBody] UserSignUpRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _registrationService.RegisterManagerAsync(request.Email, request.Password);

            var response = new UserSignUpResponse
                           {
                               Email = request.Email,
                               Role = ApplicationRoles.Manager,
                               IsRegistrationSuccessful = result
                           };

            if (result)
                return response;

            response.ResponseException = "Not successful registration. Check logs";

            return response;
        }
    }
}