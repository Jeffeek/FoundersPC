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
        public ActionResult LoginIndex() => View();

        public IActionResult RegisterIndex() => View();

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequest authenticationRequest)
        {
            if (!ModelState.IsValid) return ValidationProblem("Not valid credentials", nameof(authenticationRequest));

            var result = await ApplicationMicroservicesContext.IdentityServerClient.PostAsJsonAsync("authAPI/login", authenticationRequest);

            if (!result.IsSuccessStatusCode) return NotFound(authenticationRequest);

            var content = await result.Content.ReadFromJsonAsync<UserLoginResponse>();

            if (content == null) return NotFound();

            if (!content.IsUserExists) return NotFound();

            await Authenticate(content);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<ActionResult> LogOut()
        {
            if (!User.Identity?.IsAuthenticated ?? false) return RedirectToAction("Index", "Home");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(UserLoginResponse cookieDetails)
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

        private async Task Authenticate(UserRegisterResponse cookieDetails)
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

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (!TryValidateModel(request)) return BadRequest(request);

            var registrationResult =
                await ApplicationMicroservicesContext.IdentityServerClient.PostAsJsonAsync("authAPI/registration", request);

            var deliveredResult = await registrationResult.Content.ReadFromJsonAsync<UserRegisterResponse>();

            if (ReferenceEquals(deliveredResult, null)) throw new ArgumentNullException(nameof(deliveredResult));

            if (!deliveredResult.IsRegistrationSuccessful) return Conflict(deliveredResult);

            await Authenticate(deliveredResult);

            return Ok();
        }
    }
}