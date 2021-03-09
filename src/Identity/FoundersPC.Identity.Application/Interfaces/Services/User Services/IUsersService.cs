#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.DTO;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
	public interface IUsersService
	{
		Task<IEnumerable<UserEntityReadDto>> GetAllAsync();

		Task<UserEntityReadDto> GetByIdAsync(int id);

		Task<UserEntityReadDto> FindUserByEmailAsync(string email);

		Task<UserEntityReadDto> FindUserByEmailOrLoginAndHashedPasswordAsync(string emailOrLogin, string hashedPassword);

		Task<UserEntityReadDto> FindUserByEmailOrLoginAndPasswordAsync(string emailOrLogin, string password);

		Task<bool> ChangePasswordToAsync(int userId, string newPassword, string oldHashedPassword);

		Task<bool> ChangePasswordToAsync(string userEmail, string newPassword, string oldHashedPassword);

		Task<bool> ChangePasswordToAsync(UserEntityReadDto user, string newPassword, string oldHashedPassword);

		Task<bool> ChangeLoginToAsync(string userEmail, string newLogin);

		Task<bool> ChangeLoginToAsync(UserEntityReadDto user, string newLogin);

		Task<bool> ChangeLoginToAsync(int userId, string newLogin);

		Task<bool> ChangeNotificationsToAsync(string userEmail, bool notificationOnEntrance, bool notificationOnApiRequest);

		Task<bool> ChangeNotificationsToAsync(UserEntityReadDto user, bool notificationOnEntrance, bool notificationOnApiRequest);

		Task<bool> ChangeNotificationsToAsync(int userId, bool notificationOnEntrance, bool notificationOnApiRequest);
	}
}