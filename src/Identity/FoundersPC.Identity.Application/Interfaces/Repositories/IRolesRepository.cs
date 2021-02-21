#region Using namespaces

using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Domain.Entities;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories
{
    public interface IRolesRepository : IRepositoryAsync<RoleEntity> { }
}