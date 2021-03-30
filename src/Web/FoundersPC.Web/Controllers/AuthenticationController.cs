#region Using namespaces

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using FoundersPC.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Route("Authentication")]
    [Controller]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationWebService _authenticationWebService;

        public AuthenticationController(IAuthenticationWebService authenticationWebService) =>
            _authenticationWebService = authenticationWebService;

        #region ForgotPassword

        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem("Bad email validation",
                                         nameof(model),
                                         400,
                                         "Error",
                                         nameof(ForgotPasswordViewModel));

            var forgotPasswordResponse =
                await _authenticationWebService.ForgotPasswordAsync(model);

            if (forgotPasswordResponse is null) return UnprocessableEntity();

            if (!forgotPasswordResponse.IsUserExists) return NotFound();

            if (!forgotPasswordResponse.IsConfirmationMailSent)
                return Problem(forgotPasswordResponse.Error,
                               statusCode : 500,
                               title : "Email send error");

            return View("SignIn");
        }

        #endregion

        #region SignUp

        [Route("SignUp")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SignUpAsync(SignUpViewModel signUpModel)
        {
            if (!ModelState.IsValid)
                return ValidationProblem("Bad validation/model",
                                         nameof(signUpModel),
                                         400,
                                         "Error",
                                         nameof(SignUpViewModel));

            var registrationResponse = await _authenticationWebService.SignUpAsync(signUpModel);

            if (registrationResponse is null) return UnprocessableEntity();

            if (!registrationResponse.IsRegistrationSuccessful)
                return Problem("Registration not successful",
                               nameof(signUpModel),
                               StatusCodes.Status409Conflict,
                               "Not acceptable registration",
                               nameof(UserSignUpResponse));

            await SetupDefaultCookieAsync(registrationResponse.Email, registrationResponse.Role);
            SetupJwtTokenInCookie(registrationResponse.JwtToken);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        [Route("LogOut")]
        [Authorize]
        public async Task<ActionResult> LogOutAsync()
        {
            if (!User.Identity?.IsAuthenticated ?? false) return Unauthorized();

            RemoveJwtTokenInCookie();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        #region SignIn

        [Route("SignIn")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SignInAsync(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem("Not valid credentials. Bad model.",
                                         nameof(model),
                                         StatusCodes.Status422UnprocessableEntity,
                                         "Error",
                                         nameof(SignInViewModel));

            var signInResponse = await _authenticationWebService.SignInAsync(model);

            if (signInResponse == null) return UnprocessableEntity();

            if (!signInResponse.IsUserExists) NotFound();

            if (!signInResponse.IsUserActive || signInResponse.IsUserBlocked) return Unauthorized();

            await SetupDefaultCookieAsync(signInResponse.Email, signInResponse.Role);
            SetupJwtTokenInCookie(signInResponse.JwtToken);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Cookie

        private void SetupJwtTokenInCookie(string token)
        {
            RemoveJwtTokenInCookie();

            HttpContext.Response.Cookies.Append("token",
                                                token,
                                                new CookieOptions
                                                {
                                                    HttpOnly = true,
                                                    IsEssential = true,
                                                    Secure = true
                                                });
        }

        private async Task SetupDefaultCookieAsync(string email, string role)
        {
            var claims = new List<Claim>
                         {
                             new(ClaimsIdentity.DefaultNameClaimType,
                                 email),
                             new(ClaimsIdentity.DefaultRoleClaimType,
                                 role)
                         };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(identity));
        }

        private void RemoveJwtTokenInCookie()
        {
            if (HttpContext.Request.Cookies.ContainsKey("token")) HttpContext.Response.Cookies.Delete("token");
        }

        #endregion

        #region Redirection

        [Route("SignIn")]
        [HttpGet]
        public ActionResult SignIn() =>
            User.Identity?.IsAuthenticated ?? false
                ? View("Error", new ErrorViewModel("You are already authenticated"))
                : View();

        [Route("SignUp")]
        [HttpGet]
        public IActionResult SignUp() =>
            User.Identity?.IsAuthenticated ?? false
                ? View("Error", new ErrorViewModel("You are already authenticated"))
                : View();

        [Route("ForgotPassword")]
        [HttpGet]
        public IActionResult ForgotPassword() =>
            User.Identity?.IsAuthenticated ?? false
                ? View("Error", new ErrorViewModel("You are already authenticated"))
                : View();

        #endregion
    }
}