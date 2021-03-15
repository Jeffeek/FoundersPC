#region Using namespaces

using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Services.Encryption_Services;
using FoundersPC.RequestResponseShared.Request.ChangeSettings;
using FoundersPC.RequestResponseShared.Response.ChangeSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.User.Settings
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("identityAPI/user/settings/change")]
    [ApiController]
    public class UserChangeSettingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IUsersInformationService _usersInformationService;

        public UserChangeSettingsController(IUsersInformationService usersInformationService,
                                            IMapper mapper,
                                            PasswordEncryptorService passwordEncryptorService
        )
        {
            _usersInformationService = usersInformationService;
            _mapper = mapper;
            _passwordEncryptorService = passwordEncryptorService;
        }

        [Route("password")]
        [HttpPut]
        public async Task<ActionResult<AccountSettingsChangeResponse>> ChangePassword(ChangePasswordRequest request)
        {
            if (!TryValidateModel(request))
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            var credentials = ParseJwtUserTokenCredentials();

            if (credentials.Email is null)
                return new BadRequestObjectResult(new AccountSettingsChangeResponse
                                                  {
                                                      Email = "Not found",
                                                      Operation = "Login changing",
                                                      Successful = false,
                                                      Error = "Token not valid. Email address not found"
                                                  });

            var hashedOldPassword = _passwordEncryptorService.EncryptPassword(request.OldPassword);

            var user = await _usersInformationService.FindUserByEmailOrLoginAndHashedPasswordAsync(credentials.Email, hashedOldPassword);

            if (user is null)
                return new NotFoundObjectResult(new AccountSettingsChangeResponse
                                                {
                                                    Email = credentials.Email,
                                                    Error =
                                                        $"User with email: {credentials.Email} and password: {request.OldPassword} not found. Check your password",
                                                    Operation = "Password changing",
                                                    Successful = false
                                                });

            var result = await _usersInformationService.ChangePasswordToAsync(credentials.Email, request.NewPassword, hashedOldPassword);

            return new AccountSettingsChangeResponse
                   {
                       Email = credentials.Email,
                       Successful = result,
                       Operation = "Password changing",
                       Error = result ? null : "Password changing error happened. Maybe you entered wrong old password"
                   };
        }

        [Route("login")]
        [HttpPut]
        public async Task<ActionResult<AccountSettingsChangeResponse>> ChangeLogin(ChangeLoginRequest request)
        {
            if (!TryValidateModel(request))
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            var credentials = ParseJwtUserTokenCredentials();

            if (credentials.Email is null)
                return new BadRequestObjectResult(new AccountSettingsChangeResponse
                                                  {
                                                      Email = "Not found",
                                                      Operation = "Login changing",
                                                      Successful = false,
                                                      Error = "Token not valid. Email address not found"
                                                  });

            var result = await _usersInformationService.ChangeLoginToAsync(credentials.Email, request.NewLogin);

            return new AccountSettingsChangeResponse
                   {
                       Email = credentials.Email,
                       Operation = "Login changing",
                       Successful = result
                   };
        }

        [Route("notifications")]
        [HttpPut]
        public async Task<ActionResult<AccountSettingsChangeResponse>> ChangeNotifications(ChangeNotificationsRequest request)
        {
            if (!TryValidateModel(request))
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            var credentials = ParseJwtUserTokenCredentials();

            if (credentials.Email is null)
                return BadRequest(new
                                  {
                                      error = "Token not valid. Email address not found"
                                  });

            var result = await _usersInformationService.ChangeNotificationsToAsync(credentials.Email,
                                                                        request.SendMessageOnEntrance,
                                                                        request.SendMessageOnApiRequest);

            return new AccountSettingsChangeResponse
                   {
                       Operation = "Notifications changing",
                       Successful = result,
                       Email = credentials.Email
                   };
        }

        private (string Email, string Role) ParseJwtUserTokenCredentials() =>
            (HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
             HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType));
    }
}