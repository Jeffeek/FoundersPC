#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.Web.Application.DTO;
using FoundersPC.Web.Application.DTO.Request;
using FoundersPC.Web.Application.DTO.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Controller]
    public class LoginController : Controller
    {
        public ActionResult LoginIndex() => View();

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestViewModel authenticationRequest)
        {
            if (!ModelState.IsValid) return ValidationProblem(detail : "Not valid credentials", instance : nameof(authenticationRequest));

            var result = await ApplicationMicroservicesContext.IdentityServerClient.PostAsJsonAsync<UserLoginRequestViewModel>("authAPI/login", authenticationRequest);

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
                             new Claim("Email", cookieDetails.Email),
                             new Claim("Role", cookieDetails.Role)
                         };

            var identity = new ClaimsIdentity(claims,
                                              "ApplicationCookie",
                                              "Email",
                                              "Role");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(identity));
        }
    }
}