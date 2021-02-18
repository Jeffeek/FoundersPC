using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Interfaces.Repositories.Users;
using FoundersPC.Application.Interfaces.Services.Users;
using FoundersPC.Application.Interfaces.Services.Users.Identity;
using FoundersPC.Application.ViewModels;
using FoundersPC.Domain.Entities.Users;

namespace FoundersPC.Services.Users_Services
{
	//TODO : to implement
	public class UserIdentityService : IUserIdentityService
	{
		private IUsersRepositoryAsync _repository;
		private IPasswordEncryptionService _encryptionService;
		private IMapper _mapper;

		public UserIdentityService(IUsersRepositoryAsync repository,
								   IPasswordEncryptionService passwordEncryptionService,
								   IMapper mapper)
		{
			_repository = repository;
			_encryptionService = passwordEncryptionService;
			_mapper = mapper;
		}

		#region Implementation of IUserIdentityService

		/// <inheritdoc />
		public async Task<bool> AuthorizeAsync(UserLoginViewModel user)
		{
			if (ReferenceEquals(user, null)) throw new ArgumentNullException(nameof(user));

			var hashedPass = _encryptionService.Encrypt(user.Password);
			var userWithHandledPasswordHashAndEMail = (await _repository.GetAllAsync()).SingleOrDefault(us => us.PasswordHash == hashedPass && us.Email == user.Email);

			return userWithHandledPasswordHashAndEMail != null;
		}

		/// <inheritdoc />
		public async Task<bool> RegisterAsync(UserRegisterViewModel user)
		{
			if (ReferenceEquals(user, null)) throw new ArgumentNullException(nameof(user));

			var canAdd = (await _repository.GetAllAsync()).FirstOrDefault(x => x.Email == user.Email) == null;

			if (!canAdd) return false;

			var hashedPass = _encryptionService.Encrypt(user.Password);

			var newUser = new User()
						  {
								  CreatedAt = DateTime.Now,
								  Email = user.Email,
								  PasswordHash = hashedPass,
								  RoleId = 3
						  };

			await _repository.AddAsync(newUser);

			return true;
		}

		/// <inheritdoc />
		public Task<bool> DisableAccountAsync(int id) => throw new System.NotImplementedException();

		/// <inheritdoc />
		public Task<bool> ChangeLoginAsync(int id, string newLogin) => throw new System.NotImplementedException();

		/// <inheritdoc />
		public Task<bool> ChangePasswordAsync(int id, string newPassword) => throw new System.NotImplementedException();

		#endregion
	}
}