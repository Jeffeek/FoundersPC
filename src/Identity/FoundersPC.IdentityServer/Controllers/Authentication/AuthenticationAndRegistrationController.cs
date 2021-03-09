#region Using namespaces

using System;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Services.Encryption_Services;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
	[Route("identityAPI/authentication")]
	[ApiController]
	public class AuthenticationAndRegistrationController : Controller
	{
		private readonly IMailService _mailService;
		private readonly IMapper _mapper;
		private readonly PasswordEncryptorService _passwordEncryptorService;
		private readonly IUserRegistrationService _userRegistrationService;
		private readonly IUsersEntrancesService _usersEntrancesService;
		private readonly IUsersService _usersService;

		public AuthenticationAndRegistrationController(IUsersService authenticationService,
													   IMailService mailService,
													   PasswordEncryptorService passwordEncryptorService,
													   IUserRegistrationService userRegistrationService,
													   IUsersEntrancesService usersEntrancesService,
													   IMapper mapper
		)
		{
			_usersService = authenticationService;
			_mailService = mailService;
			_passwordEncryptorService = passwordEncryptorService;
			_userRegistrationService = userRegistrationService;
			_usersEntrancesService = usersEntrancesService;
			_mapper = mapper;
		}

		[Route("forgotpassword")]
		[HttpPost]
		public async Task<UserForgotPasswordResponse> ForgotPassword(UserForgotPasswordRequest request)
		{
			if (ModelState.IsValid == false)
			{
				return new UserForgotPasswordResponse
					   {
							   EmailSendError = "Bad model",
							   Email = request.Email,
							   IsConfirmationMailSent = false,
							   IsUserExists = false
					   };
			}

			var user = await _usersService.FindUserByEmailAsync(request.Email);

			if (user == null)
			{
				return new UserForgotPasswordResponse
					   {
							   Email = request.Email,
							   EmailSendError = "User with this email doesn't exists",
							   IsConfirmationMailSent = false,
							   IsUserExists = false
					   };
			}

			var newPassword = _passwordEncryptorService.GeneratePassword();
			var updateResult = await _usersService.ChangePasswordToAsync(user.Id, newPassword, user.HashedPassword);

			if (updateResult == false)
			{
				return new UserForgotPasswordResponse
					   {
							   Email = user.Email,
							   EmailSendError = "Error happened when application tried to update the database",
							   IsUserExists = true,
							   IsConfirmationMailSent = false
					   };
			}

			var emailSendResult = await _mailService.SendNewPasswordAsync(user.Email, newPassword);

			return emailSendResult is true
						   ? new UserForgotPasswordResponse
							 {
									 Email = user.Email,
									 EmailSendError = null,
									 IsConfirmationMailSent = true,
									 IsUserExists = true
							 }
						   : new UserForgotPasswordResponse
							 {
									 Email = user.Email,
									 IsConfirmationMailSent = false,
									 IsUserExists = true,
									 EmailSendError =
											 $"New password: {newPassword}{Environment.NewLine}The password was changed. But our Email Daemon didn't send a new password to you."
							 };
		}

		[Route("registration")]
		[HttpPost]
		public async Task<UserRegisterResponse> Register(UserRegisterRequest request)
		{
			if (ReferenceEquals(request, null)) return null;

			var result = await _userRegistrationService.RegisterDefaultUserAsync(request.Email, request.Password);

			if (!result)
			{
				return new UserRegisterResponse
					   {
							   Email = request.Email,
							   IsRegistrationSuccessful = false,
							   ResponseException = $"User with email {request.Email} is already exists"
					   };
			}

			var user = await _usersService.FindUserByEmailAsync(request.Email);

			return new UserRegisterResponse
				   {
						   Email = request.Email,
						   IsRegistrationSuccessful = true,
						   ResponseException = null,
						   Role = "DefaultUser",
						   UserId = user.Id
				   };
		}

		[Route("login")]
		[HttpPost]
		public async Task<UserLoginResponse> Login(UserLoginRequest request)
		{
			if (ReferenceEquals(request, null)) return null;

			var user = await _usersService.FindUserByEmailOrLoginAndPasswordAsync(request.LoginOrEmail, request.Password);

			if (ReferenceEquals(user, null)) return new UserLoginResponse();

			await _usersEntrancesService.LogAsync(user.Id);
			if (user.SendMessageOnEntrance) await _mailService.SendEntranceNotificationAsync(user.Email);

			var mappedUser = _mapper.Map<UserEntityReadDto, UserLoginResponse>(user);
			mappedUser.IsUserExists = true;

			return mappedUser;
		}
	}
}