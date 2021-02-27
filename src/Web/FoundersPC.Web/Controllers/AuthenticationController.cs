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
        #region Redirection

        [HttpGet]
        public ActionResult LoginIndex() => User.Identity?.IsAuthenticated ?? false ? View("Stupid") : View();

        [HttpGet]
        public IActionResult RegisterIndex() => User.Identity?.IsAuthenticated ?? false ? View("Stupid") : View();

        [HttpGet]
        public IActionResult ForgotPasswordIndex() => User.Identity?.IsAuthenticated ?? false ? View("Stupid") : View();

        #endregion

        #region ForgotPassword

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(UserForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            var serverMessage =
                await ApplicationMicroservicesContext.IdentityServerClient.PostAsJsonAsync<UserForgotPasswordRequest>("authAPI/forgotpassword", request);

            var result = await serverMessage.Content.ReadFromJsonAsync<UserForgotPasswordResponse>();

            if (result is null)
                return Problem(detail : "Authentication server returned null object. Contact the administration",
                               statusCode : 404,
                               title : "Error");

            if (!result.IsUserExists)
                return NotFound(new
                                {
                                    error = $"User with email = {request.Email} does not exists in our database"
                                });

            if (!result.IsConfirmationMailSent)
                return Problem(detail : result.EmailSendError,
                               statusCode : 404,
                               title : "Error");

            return View("ForgotPasswordResult", result);
        }

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

            await SetupCookieAsync(content.Email, content.Role);

            return RedirectToAction("Index", "Home");
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

            await SetupCookieAsync(deliveredResult.Email, "DefaultUser");

            return RedirectToAction("Index", "Home");
        }

        #endregion

        [Authorize]
        public async Task<ActionResult> SignOutAsync()
        {
            if (!User.Identity?.IsAuthenticated ?? false) return RedirectToAction("Index", "Home");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        private async Task SetupCookieAsync(string email, string role)
        {
            var claims = new List<Claim>
                         {
                             new("Email", email),
                             new("Role", role)
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