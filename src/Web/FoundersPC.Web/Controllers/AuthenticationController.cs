#region Using namespaces

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.AuthenticationShared;
using FoundersPC.AuthenticationShared.Request;
using FoundersPC.AuthenticationShared.Response;
using FoundersPC.Web.Services.Web_Services;
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
        private readonly ApplicationMicroservices _applicationMicroservices;

        public AuthenticationController(ApplicationMicroservices applicationMicroservices)
        {
            _applicationMicroservices = applicationMicroservices;
        }

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
                await _applicationMicroservices.IdentityServerClient.PostAsJsonAsync("authentication/forgotpassword", request);

            if (!serverMessage.IsSuccessStatusCode)
                return Problem(detail : "Server error",
                               statusCode : (int)serverMessage.StatusCode);

            var result = await serverMessage.Content.ReadFromJsonAsync<UserForgotPasswordResponse>();

            if (result is null)
                return Problem("Authentication server returned null object. Contact the administration",
                               statusCode : 404,
                               title : "Error");

            if (!result.IsUserExists)
                return NotFound(new
                                {
                                    error = $"User with email = {request.Email} does not exists in our database"
                                });

            if (!result.IsConfirmationMailSent)
                return Problem(result.EmailSendError,
                               statusCode : 404,
                               title : "Error");

            return View("ForgotPasswordResult", result);
        }

        #endregion

        #region SignUp

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserRegisterRequest request)
        {
            if (!TryValidateModel(request)) return BadRequest(request);

            var registrationResult =
                await _applicationMicroservices.IdentityServerClient.PostAsJsonAsync("authentication/registration", request);

            var deliveredResult = await registrationResult.Content.ReadFromJsonAsync<UserRegisterResponse>();

            if (ReferenceEquals(deliveredResult, null)) throw new ArgumentNullException(nameof(deliveredResult));

            if (!deliveredResult.IsRegistrationSuccessful) return Conflict(deliveredResult);

            await SetupSessionCookieAsync(deliveredResult.Email, deliveredResult.Role, deliveredResult.UserId);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        [Authorize]
        public async Task<ActionResult> SignOutAsync()
        {
            if (!User.Identity?.IsAuthenticated ?? false) return RedirectToAction("Index", "Home");

            RemoveJwtTokenInCookie();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        #region SignIn

        [HttpPost]
        public async Task<IActionResult> LogInAsync(UserLoginRequest authenticationRequest)
        {
            if (!ModelState.IsValid) return ValidationProblem("Not valid credentials", nameof(authenticationRequest));

            var result = await _applicationMicroservices.IdentityServerClient.PostAsJsonAsync("authentication/login", authenticationRequest);

            if (!result.IsSuccessStatusCode) return NotFound(authenticationRequest);

            var content = await result.Content.ReadFromJsonAsync<UserLoginResponse>();

            if (content == null) return NotFound(result);

            if (!content.IsUserExists) return NotFound(result);

            await SetupSessionCookieAsync(content.Email, content.Role, content.UserId);
            SetupJwtTokenInCookie(content.Email, content.Role, content.UserId);

            return RedirectToAction("Index", "Home");
        }

        private void SetupJwtTokenInCookie(string contentEmail, string contentRole, int userId)
        {
            RemoveJwtTokenInCookie();
            var jwtToken = new JwtUserToken(contentEmail, contentRole, userId);
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

        private async Task SetupSessionCookieAsync(string email, string role, int userId)
        {
            var claims = new List<Claim>
                         {
                             new(ClaimsIdentity.DefaultNameClaimType, email),
                             new(ClaimsIdentity.DefaultRoleClaimType, role),
                             new(JwtRegisteredClaimNames.NameId, userId.ToString())
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
        public ActionResult LoginIndex() => User.Identity?.IsAuthenticated ?? false ? View("Stupid") : View();

        [HttpGet]
        public IActionResult RegisterIndex() => User.Identity?.IsAuthenticated ?? false ? View("Stupid") : View();

        [HttpGet]
        public IActionResult ForgotPasswordIndex() => User.Identity?.IsAuthenticated ?? false ? View("Stupid") : View();

        #endregion
    }
}