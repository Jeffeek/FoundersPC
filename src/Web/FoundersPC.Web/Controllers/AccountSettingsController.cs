using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.AccountSettings;
using FoundersPC.ApplicationShared.AccountSettings.Request;
using FoundersPC.ApplicationShared.AccountSettings.Response;
using FoundersPC.AuthenticationShared;
using FoundersPC.AuthenticationShared.Response;
using FoundersPC.Web.Models;
using FoundersPC.Web.Models.ViewModels;
using FoundersPC.Web.Services.Web_Services;
using Microsoft.AspNetCore.Authorization;

namespace FoundersPC.Web.Controllers
{
    [Authorize]
    public class AccountSettingsController : Controller
    {
        private readonly ApplicationMicroservices _applicationMicroservices;

        public AccountSettingsController(ApplicationMicroservices applicationMicroservices)
        {
            _applicationMicroservices = applicationMicroservices;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var email = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            var role = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);
            var id = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.NameId);

            if (id is null || !Int32.TryParse(id, out var intId)) throw new CookieException();

            var request = await _applicationMicroservices.IdentityServerClient.GetFromJsonAsync<UserNotificationsViewModel>($"user/settings/notifications/{intId}");
            var login = await _applicationMicroservices.IdentityServerClient.GetStringAsync($"user/settings/login/{intId}");
            var tokens = await _applicationMicroservices.IdentityServerClient.GetFromJsonAsync<IEnumerable<ApiAccessUserTokenReadDto>>($"tokens/user/{intId}");

            login = login.Trim('"');

            var settings = new UserAccountSettingsViewModel()
                           {
                               AccountInformationViewModel = new UserAccountInformationViewModel()
                                                             {
                                                                 Email = email ?? "unknown",
                                                                 Login = login,
                                                                 Role = role ?? "unknown"
                                                             },
                               LoginViewModel = new UserSecurityViewModel()
                                                {
                                                    NewLogin = login
                                                },
                               PasswordViewModel = new UserPasswordViewModel()
                                                   {
                                                       NewPassword = String.Empty,
                                                       NewPasswordConfirm = String.Empty,
                                                       OldPassword = String.Empty
                                                   },
                               NotificationsViewModel = request,
                               TokensViewModel = new UserTokensViewModel()
                                                 {
                                                     Tokens = tokens
                                                 }
                           };

            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserAccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.PasswordViewModel))
                return View("Error",
                            new ErrorViewModel()
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad model"
                            });

            var userIdClaim = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.NameId);

            if (userIdClaim is null)
                return View("Error",
                            new ErrorViewModel()
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad user id"
                            });

            var postRequest = await _applicationMicroservices.IdentityServerClient.PostAsJsonAsync<UserChangePasswordRequest>("user/settings/change/password",
                                  new UserChangePasswordRequest()
                                  {
                                      NewPassword = request.PasswordViewModel.NewPassword,
                                      OldPassword = request.PasswordViewModel.OldPassword,
                                      UserId = Int32.Parse(userIdClaim)
                                  });

            var response = await postRequest.Content.ReadFromJsonAsync<UserSettingsChangeResponse>();

            if (!response?.Successful ?? false)
                return View("Error",
                            new ErrorViewModel()
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = response?.Error ?? "Reading json error"
                            });

            return RedirectToAction("Profile", "AccountSettings");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeLogin(UserAccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.LoginViewModel))
                return View("Error",
                            new ErrorViewModel()
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad model"
                            });

            var userIdClaim = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.NameId);

            if (userIdClaim is null)
                return View("Error",
                            new ErrorViewModel()
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad user id"
                            });

            var postRequest = await _applicationMicroservices
                                    .IdentityServerClient
                                    .PostAsJsonAsync<UserChangeLoginRequest>("user/settings/change/login",
                                                                             new UserChangeLoginRequest()
                                                                             {
                                                                                 UserId = Int32.Parse(userIdClaim),
                                                                                 NewLogin = request.LoginViewModel.NewLogin
                                                                             });

            var response = await postRequest.Content.ReadFromJsonAsync<UserSettingsChangeResponse>();

            if (!response?.Successful ?? false)
                return View("Error",
                            new ErrorViewModel()
                            {
                                Error = response.Operation,
                                RequestId = HttpContext.Request.Path
                            });

            return RedirectToAction("Profile", "AccountSettings");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeNotifications(UserAccountSettingsViewModel request)
        {
            if (!TryValidateModel(request.NotificationsViewModel))
                return View("Error",
                            new ErrorViewModel()
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad model"
                            });

            var userIdClaim = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.NameId);

            if (userIdClaim is null)
                return View("Error",
                            new ErrorViewModel()
                            {
                                RequestId = HttpContext.Request.Path,
                                Error = "Bad user id"
                            });

            var postRequest = await _applicationMicroservices
                                    .IdentityServerClient
                                    .PostAsJsonAsync<UserChangeNotificationsRequest>("user/settings/change/notifications",
                                                                                     new UserChangeNotificationsRequest()
                                                                                     {
                                                                                         UserId = Int32.Parse(userIdClaim),
                                                                                         SendMessageOnApiRequest = request.NotificationsViewModel.SendNotificationOnUsingAPI,
                                                                                         SendMessageOnEntrance = request.NotificationsViewModel.SendNotificationOnEntrance
                                                                                     });

            var response = await postRequest.Content.ReadFromJsonAsync<UserSettingsChangeResponse>();

            if (!response?.Successful ?? false)
                return View("Error",
                            new ErrorViewModel()
                            {
                                Error = response.Operation,
                                RequestId = HttpContext.Request.Path
                            });

            return RedirectToAction("Profile", "AccountSettings");
        }
    }
}
