#region Using namespaces

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.RequestResponseShared.Request.ChangeSettings;
using FoundersPC.RequestResponseShared.Response.ChangeSettings;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;
using FoundersPC.Web.Models;
using FoundersPC.Web.Services.Web_Services;
using FoundersPC.Web.Services.Web_Services.Identity;
using FoundersPC.Web.Services.Web_Services.Identity.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Authorize]
    public class AccountSettingsController : Controller
    {
        private readonly IIdentityUserInformationService _userInformationService;
        private readonly IIdentityUserSettingsChangeService _settingsChangeService;
        
        
        public AccountSettingsController(IIdentityUserInformationService userInformationService, IIdentityUserSettingsChangeService settingsChangeService)
        {
            _userInformationService = userInformationService;
            _settingsChangeService = settingsChangeService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var emailInCookie = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            var roleInCookie = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);

            if (emailInCookie is null) throw new CookieException();

            var notifications = await _userInformationService.GetUserNotificationsAsync(emailInCookie);

            var login = await _userInformationService.GetUserLoginAsync(emailInCookie);

            var tokens = await _userInformationService.GetUserTokensAsync(emailInCookie);

            var settings = new AccountSettingsViewModel
                           {
                               AccountInformationViewModel = new AccountInformationViewModel
                                                             {
                                                                 Email = emailInCookie,
                                                                 Login = login,
                                                                 Role = roleInCookie ?? "unknown"
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

            var userEmailClaim = User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

            if (userEmailClaim is null)
                return View("Error",
                            new ErrorViewModel
                            {
                                    RequestId = HttpContext.Request.Path,
                                    Error = "Bad user email"
                            });

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();

            var response = await _settingsChangeService.ChangePasswordAsync(request.PasswordSettingsViewModel,
                                                                            userEmailClaim,
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

            var userEmailClaim = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();
            
            if (userEmailClaim is null)
                return View("Error",
                            new ErrorViewModel
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad user id"
                            });

            var response = await _settingsChangeService.ChangeLoginAsync(request.LoginSettingsViewModel,
                                                                         userEmailClaim,
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

            var userEmailClaim = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            if (token is null) return BadRequest();

            if (userEmailClaim is null)
                return View("Error",
                            new ErrorViewModel
                            {
                                    RequestId = HttpContext.Request.Path,
                                    Error = "Bad user id"
                            });

            var response = await _settingsChangeService.ChangeNotificationsAsync(request.NotificationsSettingsViewModel,
                                                                                 userEmailClaim,
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