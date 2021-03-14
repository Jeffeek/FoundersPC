#region Using namespaces

using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;
using FoundersPC.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Authorize]
    public class AccountSettingsController : Controller
    {
        private readonly ILogger<AccountSettingsController> _logger;
        private readonly IIdentityUserSettingsChangeService _settingsChangeService;
        private readonly IIdentityUserInformationService _userInformationService;

        public AccountSettingsController(IIdentityUserInformationService userInformationService,
                                         IIdentityUserSettingsChangeService settingsChangeService,
                                         ILogger<AccountSettingsController> logger
        )
        {
            _userInformationService = userInformationService;
            _settingsChangeService = settingsChangeService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var emailInCookie = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            var roleInCookie = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);
            Request.Cookies.TryGetValue("token", out var jwtToken);

            if (emailInCookie is null)
            {
                _logger.LogError($"Email cookie not found. ConnectionId: {HttpContext.Connection.Id}");

                throw new CookieException();
            }

            if (roleInCookie is null)
            {
                _logger.LogError($"Role cookie not found. ConnectionId: {HttpContext.Connection.Id}");

                throw new CookieException();
            }

            if (jwtToken is null)
            {
                _logger.LogError($"Jwt cookie not found. ConnectionId: {HttpContext.Connection.Id}");

                throw new CookieException();
            }

            var notifications = await _userInformationService.GetUserNotificationsAsync(emailInCookie, jwtToken);

            var login = await _userInformationService.GetUserLoginAsync(emailInCookie, jwtToken);

            var tokens = await _userInformationService.GetUserTokensAsync(emailInCookie, jwtToken);

            var settings = new AccountSettingsViewModel
                           {
                               AccountInformationViewModel = new AccountInformationViewModel
                                                             {
                                                                 Email = emailInCookie,
                                                                 Login = login,
                                                                 Role = roleInCookie
                                                             },
                               LoginSettingsViewModel = new SecuritySettingsViewModel
                                                        {
                                                            NewLogin = login
                                                        },
                               PasswordSettingsViewModel = new PasswordSettingsViewModel
                                                           {
                                                               NewPassword = String.Empty,
                                                               NewPasswordConfirm = String.Empty,
                                                               OldPassword = String.Empty
                                                           },
                               NotificationsSettingsViewModel = notifications,
                               TokensViewModel = new UserTokensViewModel
                                                 {
                                                     Tokens = tokens
                                                 }
                           };

            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.PasswordSettingsViewModel))
                return View("Error",
                            new ErrorViewModel
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad model"
                            });

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();

            var response = await _settingsChangeService.ChangePasswordAsync(request.PasswordSettingsViewModel,
                                                                            token);

            if (response is null) return BadRequest();

            if (!response.Successful)
                return View("Error",
                            new ErrorViewModel
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = response?.Error ?? "Reading json error"
                            });

            return RedirectToAction("Profile", "AccountSettings");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeLogin(AccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.LoginSettingsViewModel))
                return View("Error",
                            new ErrorViewModel
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad model"
                            });

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();

            var response = await _settingsChangeService.ChangeLoginAsync(request.LoginSettingsViewModel,
                                                                         token);

            if (response is null) return BadRequest();

            if (!response.Successful)
                return View("Error",
                            new ErrorViewModel
                            {
                                Error = response.Operation,
                                RequestId = HttpContext.Request.Path
                            });

            return RedirectToAction("Profile", "AccountSettings");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeNotifications(AccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.NotificationsSettingsViewModel))
                return View("Error",
                            new ErrorViewModel
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad model"
                            });

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();

            var response = await _settingsChangeService.ChangeNotificationsAsync(request.NotificationsSettingsViewModel,
                                                                                 token);

            if (response is null) return BadRequest();

            if (!response.Successful)
                return View("Error",
                            new ErrorViewModel
                            {
                                Error = response.Operation,
                                RequestId = HttpContext.Request.Path
                            });

            return RedirectToAction("Profile", "AccountSettings");
        }
    }
}