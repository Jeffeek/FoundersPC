#region Using namespaces

using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Services.Encryption_Services;
using FoundersPC.RequestResponseShared.Request.ChangeSettings;
using FoundersPC.RequestResponseShared.Response.ChangeSettings;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.User
{
	[Route("identityAPI/user/settings/change")]
	[ApiController]
	public class UserChangeSettingsController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly PasswordEncryptorService _passwordEncryptorService;
		private readonly IUsersService _usersService;

		public UserChangeSettingsController(IUsersService usersService,
											IMapper mapper,
											PasswordEncryptorService passwordEncryptorService
		)
		{
			_usersService = usersService;
			_mapper = mapper;
			_passwordEncryptorService = passwordEncryptorService;
		}

		[Route("password")]
		[HttpPost]
		public async Task<ActionResult<UserSettingsChangeResponse>> ChangePassword(UserChangePasswordRequest request)
		{
			if (!TryValidateModel(request))
			{
				return BadRequest(new
								  {
										  error = "Bad model"
								  });
			}

			var hashedOldPassword = _passwordEncryptorService.EncryptPassword(request.OldPassword);
			var result = await _usersService.ChangePasswordToAsync(request.Email, request.NewPassword, hashedOldPassword);

			return new UserSettingsChangeResponse
				   {
						   Email = request.Email,
						   Successful = result,
						   Operation = "Password changing",
						   Error = result ? null : "Password changing error happened. Maybe you entered wrong old password"
				   };
		}

		[Route("login")]
		[HttpPost]
		public async Task<ActionResult<UserSettingsChangeResponse>> ChangeLogin(UserChangeLoginRequest request)
		{
			if (!TryValidateModel(request))
			{
				return BadRequest(new
								  {
										  error = "Bad model"
								  });
			}

			var result = await _usersService.ChangeLoginToAsync(request.Email, request.NewLogin);

			return new UserSettingsChangeResponse
				   {
						   Email = request.Email,
						   Operation = "Login changing",
						   Successful = result
				   };
		}

		[Route("notifications")]
		[HttpPost]
		public async Task<ActionResult<UserSettingsChangeResponse>> ChangeNotifications(UserChangeNotificationsRequest request)
		{
			if (!TryValidateModel(request))
			{
				return BadRequest(new
								  {
										  error = "Bad model"
								  });
			}

			var result = await _usersService.ChangeNotificationsToAsync(request.Email, request.SendMessageOnEntrance, request.SendMessageOnApiRequest);

			return new UserSettingsChangeResponse
				   {
						   Operation = "Notifications changing",
						   Successful = result,
						   Email = request.Email
				   };
		}
	}
}