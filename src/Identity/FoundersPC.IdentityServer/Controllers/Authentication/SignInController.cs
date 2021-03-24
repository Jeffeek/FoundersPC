#region Using namespaces

using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [Route("FoundersPCIdentity/Authentication")]
    public class SignInController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ILogger<SignInController> _logger;
        private readonly IMapper _mapper;
        private readonly IUsersEntrancesService _usersEntrancesService;

        public SignInController(IAuthenticationService authenticationService,
                                IUsersEntrancesService usersEntrancesService,
                                IMapper mapper,
                                JwtConfiguration jwtConfiguration,
                                ILogger<SignInController> logger)
        {
            _authenticationService = authenticationService;
            _usersEntrancesService = usersEntrancesService;
            _mapper = mapper;
            _jwtConfiguration = jwtConfiguration;
            _logger = logger;
        }

        [Route("SignIn")]
        [HttpPost]
        public async Task<ActionResult<UserLoginResponse>> SignIn([FromBody] UserSignInRequest request)
        {
            if (!ModelState.IsValid) return UnprocessableEntity();

            var user =
                await _authenticationService.FindUserByEmailOrLoginAndPasswordAsync(request.LoginOrEmail,
                    request.Password);

            if (user is null)
            {
                _logger.LogWarning($"{nameof(SignInController)}: user with email or login = {request.LoginOrEmail} and wrote password not exist.");

                return new UserLoginResponse
                       {
                           IsUserExists = false,
                           IsUserActive = false,
                           IsUserBlocked = false,
                           MetaInfo =
                               $"User not found with request credentials. {nameof(request.LoginOrEmail)}:{request.LoginOrEmail}"
                       };
            }

            _logger.LogInformation($"{nameof(SignInController)}: user with email or login = {request.LoginOrEmail} entered the service");
            await _usersEntrancesService.LogAsync(user.Id);

            var token = new JwtUserToken(_jwtConfiguration)
                        {
                            Email = user.Email,
                            Role = user.Role.RoleTitle
                        };

            var userLoginResponse = _mapper.Map<UserEntityReadDto, UserLoginResponse>(user);
            userLoginResponse.IsUserExists = true;
            userLoginResponse.JwtToken = token.GetToken();

            return Json(userLoginResponse);
        }
    }
}