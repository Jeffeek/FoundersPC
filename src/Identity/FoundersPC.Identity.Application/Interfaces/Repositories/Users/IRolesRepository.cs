#region Using namespaces

using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Domain.Entities.Users;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Users
{
    public interface IRolesRepository : IRepositoryAsync<RoleEntity> { }
}