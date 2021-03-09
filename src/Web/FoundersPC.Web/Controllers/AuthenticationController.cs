#region Using namespaces

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.Web.Models.ViewModels.Authentication;
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
										IMapper mapper)
		{
			_applicationMicroservices = applicationMicroservices;
			_mapper = mapper;
		}

		#region ForgotPassword

		[HttpPost]
		public async Task<ActionResult> ForgotPassword(UserForgotPasswordRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new
								  {
										  error = "Bad model"
								  });
			}

			var serverMessage =
					await _applicationMicroservices.IdentityServer.PostAsJsonAsync("authentication/forgotpassword", request);

			if (!serverMessage.IsSuccessStatusCode)
			{
				return Problem("Server error",
							   statusCode :(int)serverMessage.StatusCode);
			}

			var result = await serverMessage.Content.ReadFromJsonAsync<UserForgotPasswordResponse>();

			if (result is null)
			{
				return Problem("Authentication server returned null object. Contact the administration",
							   statusCode :404,
							   title :"Error");
			}

			if (!result.IsUserExists)
			{
				return NotFound(new
								{
										error = $"User with email = {request.Email} does not exists in our database"
								});
			}

			if (!result.IsConfirmationMailSent)
			{
				return Problem(result.EmailSendError,
							   statusCode :404,
							   title :"Error");
			}

			return View("ForgotPasswordResult", result);
		}

		#endregion

		#region SignUp

		[HttpPost]
		public async Task<IActionResult> RegisterAsync(SignUpViewModel signUpModel)
		{
			if (!TryValidateModel(signUpModel)) return BadRequest(signUpModel);

			var request = _mapper.Map<SignUpViewModel, UserRegisterRequest>(signUpModel);

			var registrationResult =
					await _applicationMicroservices.IdentityServer.PostAsJsonAsync("authentication/registration", request);

			var content = await registrationResult.Content.ReadFromJsonAsync<UserRegisterResponse>();

			if (ReferenceEquals(content, null))
			{
				return Problem("Deserialize error",
							   nameof(content),
							   StatusCodes.Status500InternalServerError,
							   "Response error",
							   nameof(UserRegisterResponse));
			}

			if (!content.IsRegistrationSuccessful)
			{
				return Problem("Registration not successful",
							   nameof(signUpModel),
							   StatusCodes.Status409Conflict,
							   "Not acceptable registration",
							   nameof(UserRegisterResponse));
			}

			await SetupSessionCookieAsync(content.Email, content.Role);

			return RedirectToAction("Index", "Home");
		}

		#endregion

		[Authorize]
		public async Task<ActionResult> LogoutAsync()
		{
			if (!User.Identity?.IsAuthenticated ?? false) return RedirectToAction("Index", "Home");

			RemoveJwtTokenInCookie();
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Index", "Home");
		}

		#region SignIn

		[HttpPost]
		public async Task<IActionResult> SigninAsync(SignInViewModel signInModel)
		{
			if (!ModelState.IsValid) return ValidationProblem("Not valid credentials", nameof(signInModel));

			var request = _mapper.Map<SignInViewModel, UserLoginRequest>(signInModel);

			var result = await _applicationMicroservices.IdentityServer.PostAsJsonAsync("authentication/login", request);

			if (!result.IsSuccessStatusCode) return NotFound(request);

			var content = await result.Content.ReadFromJsonAsync<UserLoginResponse>();

			if (content == null)
			{
				return Problem("Deserialize error",
							   nameof(content),
							   StatusCodes.Status500InternalServerError,
							   "Response error",
							   nameof(UserLoginResponse));
			}

			if (!content.IsUserExists)
			{
				return NotFound(new
								{
										error = "User not exists"
								});
			}

			await SetupSessionCookieAsync(content.Email, content.Role);
			SetupJwtTokenInCookie(content.Email, content.Role);

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
		public ActionResult Signin() => User.Identity?.IsAuthenticated ?? false ? View("Stupid") : View();

		[HttpGet]
		public IActionResult SignUp() => User.Identity?.IsAuthenticated ?? false ? View("Stupid") : View();

		[HttpGet]
		public IActionResult ForgotPassword() => User.Identity?.IsAuthenticated ?? false ? View("Stupid") : View();

		#endregion
	}
}