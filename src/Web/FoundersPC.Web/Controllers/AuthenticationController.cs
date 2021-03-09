#region Using namespaces

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
        private readonly IMapper _mapper;

        public AuthenticationController(ApplicationMicroservices applicationMicroservices,
                                        IMapper mapper
        )
        {
            _applicationMicroservices = applicationMicroservices;
            _mapper = mapper;
        }

        #region ForgotPassword

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            var requestModel = _mapper.Map<ForgotPasswordViewModel, UserForgotPasswordRequest>(model);

            var serverMessage =
                await _applicationMicroservices.IdentityServer.PostAsJsonAsync("authentication/forgotpassword",
                                                                               requestModel);

            if (!serverMessage.IsSuccessStatusCode)
                return Problem("Server error",
                               statusCode : (int)serverMessage.StatusCode);

            var result = await serverMessage.Content.ReadFromJsonAsync<UserForgotPasswordResponse>();

            if (result is null)
                return Problem("Authentication server returned null object. Contact the administration",
                               statusCode : 404,
                               title : "Error");

            if (!result.IsUserExists)
                return NotFound(new
                                {
                                    error = $"User with email = {model.Email} does not exists in our database"
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
        public async Task<IActionResult> SignUpAsync(SignUpViewModel signUpModel)
        {
            if (!TryValidateModel(signUpModel)) return BadRequest(signUpModel);

            var requestModel = _mapper.Map<SignUpViewModel, UserSignUpRequest>(signUpModel);

            var registrationRequest =
                await _applicationMicroservices.IdentityServer.PostAsJsonAsync("authentication/registration", requestModel);

            var registerResponseContent = await registrationRequest.Content.ReadFromJsonAsync<UserRegisterResponse>();

            if (ReferenceEquals(registerResponseContent, null))
                return Problem("Deserialize error",
                               nameof(registerResponseContent),
                               StatusCodes.Status500InternalServerError,
                               "Response error",
                               nameof(UserRegisterResponse));

            if (!registerResponseContent.IsRegistrationSuccessful)
                return Problem("Registration not successful",
                               nameof(signUpModel),
                               StatusCodes.Status409Conflict,
                               "Not acceptable registration",
                               nameof(UserRegisterResponse));

            await SetupSessionCookieAsync(registerResponseContent.Email, registerResponseContent.Role);

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
            if (!ModelState.IsValid) return ValidationProblem("Not valid credentials",
                                                              nameof(model));

            var signInModel = _mapper.Map<SignInViewModel, UserSignInRequest>(model);

            var signInRequest = await _applicationMicroservices.IdentityServer.PostAsJsonAsync("authentication/login", signInModel);

            if (!signInRequest.IsSuccessStatusCode) return BadRequest(signInModel);

            var userLoginResponseContent = await signInRequest.Content.ReadFromJsonAsync<UserLoginResponse>();

            if (userLoginResponseContent == null)
                return Problem("Deserialize error",
                               nameof(userLoginResponseContent),
                               StatusCodes.Status500InternalServerError,
                               "Response error",
                               nameof(UserLoginResponse));

            if (!userLoginResponseContent.IsUserExists)
                return NotFound(new
                                {
                                    error = "User not exists"
                                });

            await SetupSessionCookieAsync(userLoginResponseContent.Email, userLoginResponseContent.Role);
            SetupJwtTokenInCookie(userLoginResponseContent.Email, userLoginResponseContent.Role);

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