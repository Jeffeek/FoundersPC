﻿#region Using namespaces

using System;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Web.Application;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services.Users;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User;
using FoundersPC.Web.Domain.Common.AccountSettings;
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
            var (emailInCookie, _) = HttpContext.ParseCredentials();

            var information =
                await _usersInformationService.GetUserByEmailAsync(emailInCookie, HttpContext.GetJwtTokenFromCookie());

            if (information is null)
                return RedirectToPagePermanent("Forbidden");

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
            if (!TryValidateModel(request.PasswordSettingsViewModel))
                return RedirectToPage("Error");

            var response = await _settingsChangeWebService.ChangePasswordAsync(request.PasswordSettingsViewModel,
                                                                               HttpContext.GetJwtTokenFromCookie());

            if (response is null)
                return BadRequest();

            if (!response.Successful)
                return RedirectToPage("Error");

            return RedirectToAction("Profile", "Account");
        }

        [Route("ChangeLogin")]
        [HttpPost]
        public async Task<IActionResult> ChangeLogin([FromForm] AccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.LoginSettingsViewModel))
                return RedirectToPage("Error");

            var response = await _settingsChangeWebService.ChangeLoginAsync(request.LoginSettingsViewModel,
                                                                            HttpContext.GetJwtTokenFromCookie());

            if (response is null)
                return BadRequest();

            if (!response.Successful)
                return RedirectToPage("Error");

            return RedirectToAction("Profile", "Account");
        }

        [Route("ChangeNotifications")]
        [HttpPost]
        public async Task<IActionResult> ChangeNotifications([FromForm] AccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.NotificationsSettingsViewModel))
                return RedirectToPage("Error");

            var response =
                await _settingsChangeWebService.ChangeNotificationsAsync(request.NotificationsSettingsViewModel,
                                                                         HttpContext.GetJwtTokenFromCookie());

            if (response is null)
                return BadRequest();

            if (!response.Successful)
                return RedirectToPage("Error");

            return RedirectToAction("Profile", "Account");
        }
    }
}