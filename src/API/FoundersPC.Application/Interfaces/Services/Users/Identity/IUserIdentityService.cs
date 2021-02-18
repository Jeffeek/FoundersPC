using System.Threading.Tasks;
using FoundersPC.Application.ViewModels;

namespace FoundersPC.Application.Interfaces.Services.Users.Identity
{
	public interface IUserIdentityService
	{
		Task<bool> AuthorizeAsync(UserLoginViewModel user);

		Task<bool> RegisterAsync(UserRegisterViewModel user);

		Task<bool> DisableAccountAsync(int id);

		Task<bool> ChangeLoginAsync(int id, string newLogin);

		Task<bool> ChangePasswordAsync(int id, string newPassword);
	}
}