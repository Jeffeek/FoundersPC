#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Repositories.Logs;
using FoundersPC.Identity.Application.Interfaces.Repositories.Tokens;
using FoundersPC.Identity.Application.Interfaces.Repositories.Users;

#endregion

namespace FoundersPC.Identity.Infrastructure.UnitOfWork
{
	public interface IUnitOfWorkUsersIdentity
	{
		IAccessTokensLogsRepository AccessTokensLogsRepository { get; }

		IUsersEntrancesLogsRepository UsersEntrancesLogsRepository { get; }

		IApiAccessUsersTokensRepository ApiAccessUsersTokensRepository { get; }

		IUsersRepository UsersRepository { get; }

		IRolesRepository RolesRepository { get; }

		Task<int> SaveChangesAsync();
	}
}