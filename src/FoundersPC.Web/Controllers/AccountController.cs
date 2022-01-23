#region Using namespaces

using System;
using System.Threading.Tasks;
using FoundersPC.SharedKernel.ApplicationConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Controllers
{
    //[Route("Account")]
    //[Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
    //public class AccountController : Controller
    //{
    //    private readonly ILogger<AccountController> _logger;
    //    private readonly IUserSettingsChangeWebService _settingsChangeWebService;
    //    private readonly IUsersInformationService _usersInformationService;

    //    public AccountController(IUserSettingsChangeWebService settingsChangeWebService,
    //                             ILogger<AccountController> logger,
    //                             IUsersInformationService usersInformationService)
    //    {
    //        _settingsChangeWebService = settingsChangeWebService;
    //        _logger = logger;
    //        _usersInformationService = usersInformationService;
    //    }

    //    public async Task<IActionResult> Profile()
    //    {
    //        var (emailInCookie, _) = HttpContext.ParseCredentials();

    //        var information =
    //            await _usersInformationService.GetUserByEmailAsync(emailInCookie, HttpContext.GetJwtTokenFromCookie());

    //        if (information is null)
    //        {
    //            _logger.LogWarning("Profile page requested, but information was null. Redirecting..");

    //            return RedirectToPagePermanent("Forbidden");
    //        }

    //        var settings = new AccountSettingsViewModel
    //                       {
    //                           AccountInformationViewModel = new AccountInformationViewModel
    //                                                         {
    //                                                             Email = emailInCookie,
    //                                                             Login = information.Login,
    //                                                             Role = information.Role.RoleTitle
    //                                                         },
    //                           LoginSettingsViewModel = new SecuritySettingsViewModel
    //                                                    {
    //                                                        NewLogin = information.Login
    //                                                    },
    //                           PasswordSettingsViewModel = new PasswordSettingsViewModel
    //                                                       {
    //                                                           NewPassword = String.Empty,
    //                                                           NewPasswordConfirm = String.Empty,
    //                                                           OldPassword = String.Empty
    //                                                       },
    //                           NotificationsSettingsViewModel = new NotificationsSettingsViewModel
    //                                                            {
    //                                                                SendNotificationOnEntrance =
    //                                                                    information.SendMessageOnEntrance,
    //                                                                SendNotificationOnUsingAPI =
    //                                                                    information.SendMessageOnApiRequest
    //                                                            },
    //                           TokensViewModel = new UserTokensViewModel
    //                                             {
    //                                                 Tokens = information.Tokens
    //                                             }
    //                       };

    //        return View(settings);
    //    }

    //    [Route("ChangePassword")]
    //    [HttpPost]
    //    public async Task<IActionResult> ChangePassword([FromForm] AccountSettingsViewModel request)
    //    {
    //        var response = await _settingsChangeWebService.ChangePasswordAsync(request.PasswordSettingsViewModel,
    //                                                                           HttpContext.GetJwtTokenFromCookie());

    //        if (response is null)
    //        {
    //            _logger.LogWarning("Response of changing password was null..");

    //            return BadRequest();
    //        }

    //        if (response.Successful)
    //            return RedirectToAction("Profile", "Account");

    //        _logger.LogWarning("Response of changing password was NOT null, but not successful..");

    //        return RedirectToPage("Error");
    //    }

    //    [Route("ChangeLogin")]
    //    [HttpPost]
    //    public async Task<IActionResult> ChangeLogin([FromForm] AccountSettingsViewModel request)
    //    {
    //        var response = await _settingsChangeWebService.ChangeLoginAsync(request.LoginSettingsViewModel,
    //                                                                        HttpContext.GetJwtTokenFromCookie());

    //        if (response is null)
    //        {
    //            _logger.LogWarning("Response of changing login was null..");

    //            return BadRequest();
    //        }

    //        if (response.Successful)
    //            return RedirectToAction("Profile", "Account");

    //        _logger.LogWarning("Response of changing login was NOT null, but not successful..");

    //        return RedirectToPage("Error");
    //    }

    //    [Route("ChangeNotifications")]
    //    [HttpPost]
    //    public async Task<IActionResult> ChangeNotifications([FromForm] AccountSettingsViewModel request)
    //    {
    //        var response =
    //            await _settingsChangeWebService.ChangeNotificationsAsync(request.NotificationsSettingsViewModel,
    //                                                                     HttpContext.GetJwtTokenFromCookie());

    //        if (response is null)
    //        {
    //            _logger.LogWarning("Response of changing notifications was null..");

    //            return BadRequest();
    //        }

    //        if (response.Successful)
    //            return RedirectToAction("Profile", "Account");

    //        _logger.LogWarning("Response of changing notifications was NOT null, but not successful..");

    //        return RedirectToPage("Error");
    //    }
    //}
}