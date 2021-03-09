#region Using namespaces

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.RequestResponseShared.Request.ChangeSettings;
using FoundersPC.RequestResponseShared.Response.ChangeSettings;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;
using FoundersPC.Web.Models;
using FoundersPC.Web.Services.Web_Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Authorize]
    public class AccountSettingsController : Controller
    {
        private readonly ApplicationMicroservices _applicationMicroservices;

        public AccountSettingsController(ApplicationMicroservices applicationMicroservices) => _applicationMicroservices = applicationMicroservices;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var email = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            var role = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);

            var request =
                await _applicationMicroservices.IdentityServer.GetFromJsonAsync<NotificationsSettingsViewModel>($"user/settings/notifications/{email}");

            if (request is null) throw new BadHttpRequestException(nameof(NotificationsSettingsViewModel));

            var login = await _applicationMicroservices.IdentityServer.GetStringAsync($"user/settings/login/{email}");

            if (login is null) throw new BadHttpRequestException("UserLogin");

            var tokens = await _applicationMicroservices.IdentityServer.GetFromJsonAsync<IEnumerable<ApiAccessUserTokenReadDto>>($"tokens/user/{email}");

            if (tokens is null) throw new BadHttpRequestException(nameof(IEnumerable<ApiAccessUserTokenReadDto>));

            login = login.Trim('"');

            var settings = new AccountSettingsViewModel
                           {
                               AccountInformationViewModel = new AccountInformationViewModel
                                                             {
                                                                 Email = email ?? "unknown",
                                                                 Login = login,
                                                                 Role = role ?? "unknown"
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
                               NotificationsSettingsViewModel = request,
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

            var postRequest = await _applicationMicroservices.IdentityServer.PostAsJsonAsync("user/settings/change/password",
                                                                                             new ChangePasswordRequest
                                                                                             {
                                                                                                 NewPassword = request.PasswordSettingsViewModel.NewPassword,
                                                                                                 OldPassword = request.PasswordSettingsViewModel.OldPassword,
                                                                                                 Email = userEmailClaim
                                                                                             });

            var response = await postRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            if (!response?.Successful ?? false)
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

            if (userEmailClaim is null)
                return View("Error",
                            new ErrorViewModel
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad user id"
                            });

            var postRequest = await _applicationMicroservices
                                    .IdentityServer
                                    .PostAsJsonAsync("user/settings/change/login",
                                                     new ChangeLoginRequest
                                                     {
                                                         Email = userEmailClaim,
                                                         NewLogin = request.LoginSettingsViewModel.NewLogin
                                                     });

            var response = await postRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            if (!response?.Successful ?? false)
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

            var userEmailClaim = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.NameId);

            if (userEmailClaim is null)
                return View("Error",
                            new ErrorViewModel
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad user email"
                            });

            var postRequest = await _applicationMicroservices
                                    .IdentityServer
                                    .PostAsJsonAsync("user/settings/change/notifications",
                                                     new ChangeNotificationsRequest
                                                     {
                                                         Email = userEmailClaim,
                                                         SendMessageOnApiRequest = request.NotificationsSettingsViewModel.SendNotificationOnUsingAPI,
                                                         SendMessageOnEntrance = request.NotificationsSettingsViewModel.SendNotificationOnEntrance
                                                     });

            var response = await postRequest.Content.ReadFromJsonAsync<AccountSettingsChangeResponse>();

            if (!response?.Successful ?? false)
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