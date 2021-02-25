#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.AuthenticationShared.Request;
using FoundersPC.AuthenticationShared.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Controller]
    public class AuthenticationController : Controller
    {
        [Authorize]
        public async Task<ActionResult> SignOutAsync()
        {
            if (!User.Identity?.IsAuthenticated ?? false) return RedirectToAction("Index", "Home");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        #region Redirection

        public ActionResult LoginIndex() => User.Identity?.IsAuthenticated ?? false ? View("Stupid") : View();

        public IActionResult RegisterIndex() => User.Identity?.IsAuthenticated ?? false ? View("Stupid") : View();

        public IActionResult ForgotPassword() => View();

        #endregion

        #region SignIn

        [HttpPost]
        public async Task<IActionResult> LogInAsync(UserLoginRequest authenticationRequest)
        {
            if (!ModelState.IsValid) return ValidationProblem("Not valid credentials", nameof(authenticationRequest));

            var result = await ApplicationMicroservicesContext.IdentityServerClient.PostAsJsonAsync("authAPI/login", authenticationRequest);

            if (!result.IsSuccessStatusCode) return NotFound(authenticationRequest);

            var content = await result.Content.ReadFromJsonAsync<UserLoginResponse>();

            if (content == null) return NotFound(result);

            if (!content.IsUserExists) return NotFound(result);

            await AuthenticateAsync(content);

            return RedirectToAction("Index", "Home");
        }

        private async Task AuthenticateAsync(UserLoginResponse cookieDetails)
        {
            var claims = new List<Claim>
                         {
                             new("Email", cookieDetails.Email),
                             new("Role", cookieDetails.Role)
                         };

            var identity = new ClaimsIdentity(claims,
                                              "ApplicationCookie",
                                              "Email",
                                              "Role");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(identity));
        }

        #endregion

        #region SignUp

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserRegisterRequest request)
        {
            if (!TryValidateModel(request)) return BadRequest(request);

            var registrationResult =
                await ApplicationMicroservicesContext.IdentityServerClient.PostAsJsonAsync("authAPI/registration", request);

            var deliveredResult = await registrationResult.Content.ReadFromJsonAsync<UserRegisterResponse>();

            if (ReferenceEquals(deliveredResult, null)) throw new ArgumentNullException(nameof(deliveredResult));

            if (!deliveredResult.IsRegistrationSuccessful) return Conflict(deliveredResult);

            await AuthenticateAsync(deliveredResult);

            return RedirectToAction("Index", "Home");
        }

        private async Task AuthenticateAsync(UserRegisterResponse cookieDetails)
        {
            var claims = new List<Claim>
                         {
                             new("Email", cookieDetails.Email),
                             new("Role", "DefaultUser")
                         };

            var identity = new ClaimsIdentity(claims,
                                              "ApplicationCookie",
                                              "Email",
                                              "Role");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(identity));
        }

        #endregion
    }
}