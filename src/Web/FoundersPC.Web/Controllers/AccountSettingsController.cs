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
using FoundersPC.Web.Models;
using FoundersPC.Web.Models.ViewModels;
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
			var id = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.NameId);

			if (id is null
				|| !Int32.TryParse(id, out var intId))
			{
				throw new CookieException
					  {
							  HelpLink = "google.com"
					  };
			}

			var request =
					await _applicationMicroservices.IdentityServer.GetFromJsonAsync<UserNotificationsViewModel>($"user/settings/notifications/{intId}");

			if (request is null) throw new BadHttpRequestException(nameof(UserNotificationsViewModel));

			var login = await _applicationMicroservices.IdentityServer.GetStringAsync($"user/settings/login/{intId}");

			if (login is null) throw new BadHttpRequestException("UserLogin");

			var tokens = await _applicationMicroservices.IdentityServer.GetFromJsonAsync<IEnumerable<ApiAccessUserTokenReadDto>>($"tokens/user/{intId}");

			if (tokens is null) throw new BadHttpRequestException(nameof(IEnumerable<ApiAccessUserTokenReadDto>));

			login = login.Trim('"');

			var settings = new UserAccountSettingsViewModel
						   {
								   AccountInformationViewModel = new UserAccountInformationViewModel
																 {
																		 Email = email ?? "unknown",
																		 Login = login,
																		 Role = role ?? "unknown"
																 },
								   LoginViewModel = new UserSecurityViewModel
													{
															NewLogin = login
													},
								   PasswordViewModel = new UserPasswordViewModel
													   {
															   NewPassword = String.Empty,
															   NewPasswordConfirm = String.Empty,
															   OldPassword = String.Empty
													   },
								   NotificationsViewModel = request,
								   TokensViewModel = new UserTokensViewModel
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
			{
				return View("Error",
							new ErrorViewModel
							{
									RequestId = HttpContext.Request.Path,
									Error = "Bad model"
							});
			}

			var userEmailClaim = User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

			if (userEmailClaim is null)
			{
				return View("Error",
							new ErrorViewModel
							{
									RequestId = HttpContext.Request.Path,
									Error = "Bad user email"
							});
			}

			var postRequest = await _applicationMicroservices.IdentityServer.PostAsJsonAsync("user/settings/change/password",
																							 new UserChangePasswordRequest
																							 {
																									 NewPassword = request.PasswordViewModel.NewPassword,
																									 OldPassword = request.PasswordViewModel.OldPassword,
																									 Email = userEmailClaim
																							 });

			var response = await postRequest.Content.ReadFromJsonAsync<UserSettingsChangeResponse>();

			if (!response?.Successful ?? false)
			{
				return View("Error",
							new ErrorViewModel
							{
									RequestId = HttpContext.Request.Path,
									Error = response?.Error ?? "Reading json error"
							});
			}

			return RedirectToAction("Profile", "AccountSettings");
		}

		[HttpPost]
		public async Task<IActionResult> ChangeLogin(UserAccountSettingsViewModel request)
		{
			if (!TryValidateModel(request.LoginViewModel))
			{
				return View("Error",
							new ErrorViewModel
							{
									RequestId = HttpContext.Request.Path,
									Error = "Bad model"
							});
			}

			var userEmailClaim = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

			if (userEmailClaim is null)
			{
				return View("Error",
							new ErrorViewModel
							{
									RequestId = HttpContext.Request.Path,
									Error = "Bad user id"
							});
			}

			var postRequest = await _applicationMicroservices
									.IdentityServer
									.PostAsJsonAsync("user/settings/change/login",
													 new UserChangeLoginRequest
													 {
															 Email = userEmailClaim,
															 NewLogin = request.LoginViewModel.NewLogin
													 });

			var response = await postRequest.Content.ReadFromJsonAsync<UserSettingsChangeResponse>();

			if (!response?.Successful ?? false)
			{
				return View("Error",
							new ErrorViewModel
							{
									Error = response.Operation,
									RequestId = HttpContext.Request.Path
							});
			}

			return RedirectToAction("Profile", "AccountSettings");
		}

		[HttpPost]
		public async Task<IActionResult> ChangeNotifications(UserAccountSettingsViewModel request)
		{
			if (!TryValidateModel(request.NotificationsViewModel))
			{
				return View("Error",
							new ErrorViewModel
							{
									RequestId = HttpContext.Request.Path,
									Error = "Bad model"
							});
			}

			var userEmailClaim = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.NameId);

			if (userEmailClaim is null)
			{
				return View("Error",
							new ErrorViewModel
							{
									RequestId = HttpContext.Request.Path,
									Error = "Bad user email"
							});
			}

			var postRequest = await _applicationMicroservices
									.IdentityServer
									.PostAsJsonAsync("user/settings/change/notifications",
													 new UserChangeNotificationsRequest
													 {
															 Email = userEmailClaim,
															 SendMessageOnApiRequest = request.NotificationsViewModel.SendNotificationOnUsingAPI,
															 SendMessageOnEntrance = request.NotificationsViewModel.SendNotificationOnEntrance
													 });

			var response = await postRequest.Content.ReadFromJsonAsync<UserSettingsChangeResponse>();

			if (!response?.Successful ?? false)
			{
				return View("Error",
							new ErrorViewModel
							{
									Error = response.Operation,
									RequestId = HttpContext.Request.Path
							});
			}

			return RedirectToAction("Profile", "AccountSettings");
		}
	}
}