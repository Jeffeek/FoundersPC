#region Using namespaces

using System.Threading.Tasks;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
	public interface IUserRegistrationService
	{
		Task<bool> RegisterDefaultUserAsync(string email, string password);

		Task<bool> RegisterManagerAsync(string email, string password);
	}
}