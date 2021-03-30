#region Using namespaces

using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Route("Account")]
    [Authorize]
    public class AccountSettingsController : Controller
    {
        private readonly ILogger<AccountSettingsController> _logger;
        private readonly IUserSettingsChangeWebService _settingsChangeWebService;
        private readonly IUserSettingsWebService _userSettingsWebService;

        public AccountSettingsController(IUserSettingsWebService userSettingsWebService,
                                         IUserSettingsChangeWebService settingsChangeWebService,
                                         ILogger<AccountSettingsController> logger)
        {
            _userSettingsWebService = userSettingsWebService;
            _settingsChangeWebService = settingsChangeWebService;
            _logger = logger;
        }

        [Route("")]
        [Authorize]
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

            var information = await _userSettingsWebService.GetOverallInformation(emailInCookie, jwtToken);

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
        public async Task<IActionResult> ChangePassword(AccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.PasswordSettingsViewModel)) return RedirectToPage("Error");

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();

            var response = await _settingsChangeWebService.ChangePasswordAsync(request.PasswordSettingsViewModel,
                                                                               token);

            if (response is null) return BadRequest();

            if (!response.Successful) return RedirectToPage("Error");

            return RedirectToAction("Profile", "AccountSettings");
        }

        [Route("ChangeLogin")]
        [HttpPost]
        public async Task<IActionResult> ChangeLogin(AccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.LoginSettingsViewModel)) return RedirectToPage("Error");

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();

            var response = await _settingsChangeWebService.ChangeLoginAsync(request.LoginSettingsViewModel,
                                                                            token);

            if (response is null) return BadRequest();

            if (!response.Successful) return RedirectToPage("Error");

            return RedirectToAction("Profile", "AccountSettings");
        }

        [Route("ChangeNotifications")]
        [HttpPost]
        public async Task<IActionResult> ChangeNotifications(AccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.NotificationsSettingsViewModel)) return RedirectToPage("Error");

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();

            var response =
                await _settingsChangeWebService.ChangeNotificationsAsync(request.NotificationsSettingsViewModel,
                                                                         token);

            if (response is null) return BadRequest();

            if (!response.Successful) return RedirectToPage("Error");

            return RedirectToAction("Profile", "AccountSettings");
        }
    }
}