#region Using namespaces

using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.IdentityServer.Request.ChangeSettings;
using FoundersPC.RequestResponseShared.IdentityServer.Response.ChangeSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Users
{
    [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
    [ApiController]
    [Route(IdentityServerRoutes.Users.SettingsChange.SettingsChangeEndpoint)]
    public class ChangeSettingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserSettingsService _settingsService;

        public ChangeSettingsController(IMapper mapper,
                                        IUserSettingsService settingsService)
        {
            _mapper = mapper;
            _settingsService = settingsService;
        }

        [HttpPut(IdentityServerRoutes.Users.SettingsChange.PasswordChange)]
        public async Task<ActionResult<AccountSettingsChangeResponse>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var (email, _) = HttpContext.ParseJwtUserTokenCredentials();

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

        [HttpPut(IdentityServerRoutes.Users.SettingsChange.LoginChange)]
        public async Task<ActionResult<AccountSettingsChangeResponse>> ChangeLogin([FromBody] ChangeLoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            var (email, _) = HttpContext.ParseJwtUserTokenCredentials();

            if (email is null)
                return new BadRequestObjectResult(new AccountSettingsChangeResponse
                                                  {
                                                      Email = "Not found",
                                                      Operation = "SignIn changing",
                                                      Successful = false,
                                                      Error = "Token not valid. Email address not found"
                                                  });

            var result = await _settingsService.ChangeLoginToAsync(email, request.NewLogin);

            return new AccountSettingsChangeResponse
                   {
                       Email = email,
                       Operation = "SignIn changing",
                       Successful = result
                   };
        }

        [HttpPut(IdentityServerRoutes.Users.SettingsChange.NotificationsChange)]
        public async Task<ActionResult<AccountSettingsChangeResponse>> ChangeNotifications([FromBody] ChangeNotificationsRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            var (email, _) = HttpContext.ParseJwtUserTokenCredentials();

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
    }
}