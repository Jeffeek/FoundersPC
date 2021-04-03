#region Using namespaces

using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Route("Account")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserSettingsChangeWebService _settingsChangeWebService;
        private readonly IUsersInformationService _usersInformationService;

        public AccountController(IUserSettingsChangeWebService settingsChangeWebService,
                                 ILogger<AccountController> logger,
                                 IUsersInformationService usersInformationService)
        {
            _settingsChangeWebService = settingsChangeWebService;
            _logger = logger;
            _usersInformationService = usersInformationService;
        }

        public async Task<IActionResult> Profile()
        {
            var emailInCookie = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            Request.Cookies.TryGetValue("token", out var jwtToken);

            if (emailInCookie is null)
            {
                _logger.LogError($"Email cookie not found. ConnectionId: {HttpContext.Connection.Id}");

                throw new CookieException();
            }

            if (jwtToken is null)
            {
                _logger.LogError($"Jwt cookie not found. ConnectionId: {HttpContext.Connection.Id}");

                throw new CookieException();
            }

            var information = await _usersInformationService.GetUserByEmailAsync(emailInCookie, jwtToken);

            if (information is null) return RedirectToPagePermanent("Forbidden");

            var settings = new AccountSettingsViewModel
                           {
                               AccountInformationViewModel = new AccountInformationViewModel
                                                             {
                                                                 Email = emailInCookie,
                                                                 Login = information.Login,
                                                                 Role = information.Role.RoleTitle
                                                             },
                               LoginSettingsViewModel = new SecuritySettingsViewModel
                                                        {
                                                            NewLogin = information.Login
                                                        },
                               PasswordSettingsViewModel = new PasswordSettingsViewModel
                                                           {
                                                               NewPassword = String.Empty,
                                                               NewPasswordConfirm = String.Empty,
                                                               OldPassword = String.Empty
                                                           },
                               NotificationsSettingsViewModel = new NotificationsSettingsViewModel
                                                                {
                                                                    SendNotificationOnEntrance =
                                                                        information.SendMessageOnEntrance,
                                                                    SendNotificationOnUsingAPI =
                                                                        information.SendMessageOnApiRequest
                                                                },
                               TokensViewModel = new UserTokensViewModel
                                                 {
                                                     Tokens = information.Tokens
                                                 }
                           };

            return View(settings);
        }

        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromForm] AccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.PasswordSettingsViewModel)) return RedirectToPage("Error");

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();

            var response = await _settingsChangeWebService.ChangePasswordAsync(request.PasswordSettingsViewModel,
                                                                               token);

            if (response is null) return BadRequest();

            if (!response.Successful) return RedirectToPage("Error");

            return RedirectToAction("Profile", "Account");
        }

        [Route("ChangeLogin")]
        [HttpPost]
        public async Task<IActionResult> ChangeLogin([FromForm] AccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.LoginSettingsViewModel)) return RedirectToPage("Error");

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();

            var response = await _settingsChangeWebService.ChangeLoginAsync(request.LoginSettingsViewModel,
                                                                            token);

            if (response is null) return BadRequest();

            if (!response.Successful) return RedirectToPage("Error");

            return RedirectToAction("Profile", "Account");
        }

        [Route("ChangeNotifications")]
        [HttpPost]
        public async Task<IActionResult> ChangeNotifications([FromForm] AccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.NotificationsSettingsViewModel)) return RedirectToPage("Error");

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();

            var response =
                await _settingsChangeWebService.ChangeNotificationsAsync(request.NotificationsSettingsViewModel,
                                                                         token);

            if (response is null) return BadRequest();

            if (!response.Successful) return RedirectToPage("Error");

            return RedirectToAction("Profile", "Account");
        }
    }
}