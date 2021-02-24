#region Using namespaces

using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Domain.Entities.Logs;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Logs
{
    public interface IUsersEntrancesLogsRepository : IRepositoryAsync<UserEntranceLog> { }
}