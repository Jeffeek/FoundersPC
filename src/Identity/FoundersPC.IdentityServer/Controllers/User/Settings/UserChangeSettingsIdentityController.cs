#region Using namespaces

using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
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
    [Route("FoundersPCIdentity/User/Settings/Change")]
    [ApiController]
    public class UserChangeSettingsIdentityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IUserSettingsService _settingsService;
        private readonly IUsersInformationService _usersInformationService;

        public UserChangeSettingsIdentityController(IUsersInformationService usersInformationService,
                                                    IMapper mapper,
                                                    PasswordEncryptorService passwordEncryptorService,
                                                    IUserSettingsService settingsService)
        {
            _usersInformationService = usersInformationService;
            _mapper = mapper;
            _passwordEncryptorService = passwordEncryptorService;
            _settingsService = settingsService;
        }

        [Route("Password")]
        [HttpPut]
        public async Task<ActionResult<AccountSettingsChangeResponse>> ChangePassword(ChangePasswordRequest request)
        {
            if (!TryValidateModel(request))
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            var (email, role) = ParseJwtUserTokenCredentials();

            if (email is null)
                return new BadRequestObjectResult(new AccountSettingsChangeResponse
                                                  {
                                                      Email = "Not found",
                                                      Operation = "SignIn changing",
                                                      Successful = false,
                                                      Error = "Token not valid. Email address not found"
                                                  });

            var result = await _settingsService.ChangePasswordToAsync(email, request.NewPassword, request.OldPassword);

            return new AccountSettingsChangeResponse
                   {
                       Email = email,
                       Successful = result,
                       Operation = "Password changing",
                       Error = result ? null : "Password changing error happened. Maybe you entered wrong old password"
                   };
        }

        [Route("Login")]
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
                                                      Operation = "SignIn changing",
                                                      Successful = false,
                                                      Error = "Token not valid. Email address not found"
                                                  });

            var result = await _settingsService.ChangeLoginToAsync(credentials.Email, request.NewLogin);

            return new AccountSettingsChangeResponse
                   {
                       Email = credentials.Email,
                       Operation = "SignIn changing",
                       Successful = result
                   };
        }

        [Route("Notifications")]
        [HttpPut]
        public async Task<ActionResult<AccountSettingsChangeResponse>> ChangeNotifications(
            ChangeNotificationsRequest request)
        {
            if (!TryValidateModel(request))
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            var (email, role) = ParseJwtUserTokenCredentials();

            if (email is null)
                return BadRequest(new
                                  {
                                      error = "Token not valid. Email address not found"
                                  });

            var result =
                await _settingsService.ChangeNotificationsToAsync(email,
                                                                  _mapper
                                                                      .Map<ChangeNotificationsRequest,
                                                                          UserNotificationsSettings>(request));

            return new AccountSettingsChangeResponse
                   {
                       Operation = "Notifications changing",
                       Successful = result,
                       Email = email
                   };
        }

        private (string Email, string Role) ParseJwtUserTokenCredentials() =>
            (HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
             HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType));
    }
}