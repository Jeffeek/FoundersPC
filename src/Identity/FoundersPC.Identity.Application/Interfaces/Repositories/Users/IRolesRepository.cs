#region Using namespaces

using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Users
{
	public interface IRolesRepository : IRepositoryAsync<RoleEntity> { }
}