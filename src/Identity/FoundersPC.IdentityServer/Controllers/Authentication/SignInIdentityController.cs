#region Using namespaces

using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.ApplicationShared.Jwt;
using FoundersPC.ApplicationShared.Middleware;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Authentication;
using FoundersPC.RequestResponseShared.IdentityServer.Response.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [EnableCors(PolicyName = "WebPolicy")]
    [AllowAnonymous]
    [ApiController]
    [Route(IdentityServerRoutes.Authentication.AuthenticationEndpoint)]
    [ModelValidation]
    public class SignInIdentityController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ILogger<SignInIdentityController> _logger;
        private readonly IMapper _mapper;
        private readonly IUsersEntrancesService _usersEntrancesService;

        public SignInIdentityController(IAuthenticationService authenticationService,
                                        IUsersEntrancesService usersEntrancesService,
                                        IMapper mapper,
                                        JwtConfiguration jwtConfiguration,
                                        ILogger<SignInIdentityController> logger)
        {
            _authenticationService = authenticationService;
            _usersEntrancesService = usersEntrancesService;
            _mapper = mapper;
            _jwtConfiguration = jwtConfiguration;
            _logger = logger;
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

        [HttpPost(IdentityServerRoutes.Authentication.SignIn)]
        public async Task<ActionResult<UserLoginResponse>> SignIn([FromBody] UserSignInRequest request)
        {
            var user =
                await _authenticationService.FindUserByEmailOrLoginAndPasswordAsync(request.LoginOrEmail,
                                                                                    request.Password);

            if (user is null)
            {
                _logger.LogWarning($"{nameof(SignInIdentityController)}: user with email or login = {request.LoginOrEmail} and wrote password not exist.");

                return new UserLoginResponse
                       {
                           IsUserExists = false,
                           IsUserActive = false,
                           IsUserBlocked = false,
                           MetaInfo =
                               $"User not found with request credentials. {nameof(request.LoginOrEmail)}:{request.LoginOrEmail}"
                       };
            }

            _logger.LogInformation($"{nameof(SignInIdentityController)}: user with email or login = {request.LoginOrEmail} entered the service");
            await _usersEntrancesService.LogAsync(user.Id);

            var token = new JwtUserToken(_jwtConfiguration)
                        {
                            Email = user.Email,
                            Role = user.Role.RoleTitle
                        };

            var userLoginResponse = _mapper.Map<UserEntityReadDto, UserLoginResponse>(user);
            userLoginResponse.IsUserExists = true;
            userLoginResponse.JwtToken = token.GetToken();

            return userLoginResponse;
        }
    }
}