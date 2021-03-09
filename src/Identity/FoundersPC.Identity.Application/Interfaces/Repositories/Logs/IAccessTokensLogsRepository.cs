#region Using namespaces

using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Logs
{
	public interface IAccessTokensLogsRepository : IRepositoryAsync<AccessTokenLog> { }
}