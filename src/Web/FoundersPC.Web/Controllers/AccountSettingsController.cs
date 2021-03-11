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

        public AccountSettingsController(IIdentityUserInformationService userInformationService)
        {
            _userInformationService = userInformationService;
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
            return Ok();
            //if (!TryValidateModel(request.PasswordSettingsViewModel))
            //    return View("Error",
            //                new ErrorViewModel
            //                {
            //                    RequestId = HttpContext.Request.Path,
            //                    Error = "Bad model"
            //                });

            //var userEmailClaim = User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

            //if (userEmailClaim is null)
            //    return View("Error",
            //                new ErrorViewModel
            //                {
            //                    RequestId = HttpContext.Request.Path,
            //                    Error = "Bad user email"
            //                });

            //var postRequest = await _applicationMicroservices.IdentityServer.PostAsJsonAsync("user/settings/change/password",
            //                                                                                 new ChangePasswordRequest
            //                                                                                 {
            //                                                                                     NewPassword = request.PasswordSettingsViewModel.NewPassword,
            //                                                                                     OldPassword = request.PasswordSettingsViewModel.OldPassword,
            //                                                                                     Email = userEmailClaim
            //                                                                                 });

            //var response = await postRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            //if (!response?.Successful ?? false)
            //    return View("Error",
            //                new ErrorViewModel
            //                {
            //                    RequestId = HttpContext.Request.Path,
            //                    Error = response?.Error ?? "Reading json error"
            //                });

            //return RedirectToAction("Profile", "AccountSettings");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeLogin(AccountSettingsViewModel request)
        {
            return Ok();
            //if (!TryValidateModel(request.LoginSettingsViewModel))
            //    return View("Error",
            //                new ErrorViewModel
            //                {
            //                    RequestId = HttpContext.Request.Path,
            //                    Error = "Bad model"
            //                });

            //var userEmailClaim = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

            //if (userEmailClaim is null)
            //    return View("Error",
            //                new ErrorViewModel
            //                {
            //                    RequestId = HttpContext.Request.Path,
            //                    Error = "Bad user id"
            //                });

            //var postRequest = await _applicationMicroservices
            //                        .IdentityServer
            //                        .PostAsJsonAsync("user/settings/change/login",
            //                                         new ChangeLoginRequest
            //                                         {
            //                                             Email = userEmailClaim,
            //                                             NewLogin = request.LoginSettingsViewModel.NewLogin
            //                                         });

            //var response = await postRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            //if (!response?.Successful ?? false)
            //    return View("Error",
            //                new ErrorViewModel
            //                {
            //                    Error = response.Operation,
            //                    RequestId = HttpContext.Request.Path
            //                });

            //return RedirectToAction("Profile", "AccountSettings");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeNotifications(AccountSettingsViewModel request)
        {
            return Ok();
            //if (!TryValidateModel(request.NotificationsSettingsViewModel))
            //    return View("Error",
            //                new ErrorViewModel
            //                {
            //                    RequestId = HttpContext.Request.Path,
            //                    Error = "Bad model"
            //                });

            //var userEmailClaim = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.NameId);

            //if (userEmailClaim is null)
            //    return View("Error",
            //                new ErrorViewModel
            //                {
            //                    RequestId = HttpContext.Request.Path,
            //                    Error = "Bad user email"
            //                });

            //var postRequest = await _applicationMicroservices
            //                        .IdentityServer
            //                        .PostAsJsonAsync("user/settings/change/notifications",
            //                                         new ChangeNotificationsRequest
            //                                         {
            //                                             Email = userEmailClaim,
            //                                             SendMessageOnApiRequest = request.NotificationsSettingsViewModel.SendNotificationOnUsingAPI,
            //                                             SendMessageOnEntrance = request.NotificationsSettingsViewModel.SendNotificationOnEntrance
            //                                         });

            //var response = await postRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            //if (!response?.Successful ?? false)
            //    return View("Error",
            //                new ErrorViewModel
            //                {
            //                    Error = response.Operation,
            //                    RequestId = HttpContext.Request.Path
            //                });

            //return RedirectToAction("Profile", "AccountSettings");
        }
    }
}