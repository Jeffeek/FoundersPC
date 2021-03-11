#region Using namespaces

using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using FoundersPC.Web.Services.Web_Services;
using FoundersPC.Web.Services.Web_Services.Identity;
using FoundersPC.Web.Services.Web_Services.Identity.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Controller]
    public class AuthenticationController : Controller
    {
        private readonly IIdentityAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IMapper mapper,
                                        IIdentityAuthenticationService authenticationService
        )
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        #region ForgotPassword

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(detail : "Bad email validation",
                                         instance : nameof(model),
                                         statusCode : 400,
                                         title : "Error",
                                         type : nameof(ForgotPasswordViewModel));

            var forgotPasswordResponse =
                await _authenticationService.ForgotPasswordAsync(model);

            if (forgotPasswordResponse is null)
                return ValidationProblem("Server returned null object",
                                         instance : nameof(forgotPasswordResponse),
                                         statusCode : 400,
                                         title : "Error",
                                         type : nameof(UserForgotPasswordResponse));

            if (!forgotPasswordResponse.IsUserExists)
                return NotFound(new
                                {
                                    error = $"User with email = {model.Email} does not exists in our database"
                                });

            if (!forgotPasswordResponse.IsConfirmationMailSent)
                return Problem(forgotPasswordResponse.EmailSendError,
                               statusCode : 401,
                               title : "Email send error");

            return View("SignIn");
        }

        #endregion

        #region SignUp

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(SignUpViewModel signUpModel)
        {
            if (!ModelState.IsValid) 
                return ValidationProblem(detail : "Bad validation/model",
                                         instance : nameof(signUpModel),
                                         statusCode : 400,
                                         title : "Error",
                                         type : nameof(SignUpViewModel));

            var registrationResponse = await _authenticationService.SignUpAsync(signUpModel);

            if (ReferenceEquals(registrationResponse, null))
                return Problem("Deserialize error",
                               nameof(registrationResponse),
                               StatusCodes.Status500InternalServerError,
                               "Response error",
                               nameof(UserRegisterResponse));

            if (!registrationResponse.IsRegistrationSuccessful)
                return Problem("Registration not successful",
                               nameof(signUpModel),
                               StatusCodes.Status409Conflict,
                               "Not acceptable registration",
                               nameof(UserRegisterResponse));

            await SetupSessionCookieAsync(registrationResponse.Email, registrationResponse.Role);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        [Authorize]
        public async Task<ActionResult> LogOutAsync()
        {
            if (!User.Identity?.IsAuthenticated ?? false) return RedirectToAction("Index", "Home");

            RemoveJwtTokenInCookie();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        #region SignIn

        [HttpPost]
        public async Task<IActionResult> SignInAsync(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem("Not valid credentials. Bad model.",
                                         nameof(model),
                                         statusCode : 400,
                                         title : "Error",
                                         type : nameof(SignInViewModel));

            var signInResponse = await _authenticationService.SignInAsync(model);

            if (signInResponse == null)
                return Problem("Deserialize error",
                               nameof(signInResponse),
                               StatusCodes.Status500InternalServerError,
                               "Response error",
                               nameof(UserLoginResponse));

            if (!signInResponse.IsUserExists)
                return NotFound(new
                                {
                                    error = "User not exists"
                                });

            await SetupSessionCookieAsync(signInResponse.Email, signInResponse.Role);
            SetupJwtTokenInCookie(signInResponse.Email, signInResponse.Role);

            return RedirectToAction("Index", "Home");
        }

        private void SetupJwtTokenInCookie(string contentEmail, string contentRole)
        {
            RemoveJwtTokenInCookie();
            var jwtToken = new JwtUserToken(contentEmail, contentRole);
            var token = jwtToken.GetToken();

            HttpContext.Response.Cookies.Append("token",
                                                token,
                                                new CookieOptions
                                                {
                                                    HttpOnly = true,
                                                    IsEssential = true,
                                                    Secure = true
                                                });
        }

        #endregion

        #region Cookie

        private async Task SetupSessionCookieAsync(string email, string role)
        {
            var claims = new List<Claim>
                         {
                             new(ClaimsIdentity.DefaultNameClaimType, email),
                             new(ClaimsIdentity.DefaultRoleClaimType, role)
                         };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        private void RemoveJwtTokenInCookie()
        {
            if (HttpContext.Request.Cookies.ContainsKey("token")) HttpContext.Response.Cookies.Delete("token");
        }

        #endregion

        #region Redirection

        [HttpGet]
        public ActionResult SignIn() => User.Identity?.IsAuthenticated ?? false ? View("AccessDenied") : View();

        [HttpGet]
        public IActionResult SignUp() => User.Identity?.IsAuthenticated ?? false ? View("AccessDenied") : View();

        [HttpGet]
        public IActionResult ForgotPassword() => User.Identity?.IsAuthenticated ?? false ? View("AccessDenied") : View();

        #endregion
    }
}