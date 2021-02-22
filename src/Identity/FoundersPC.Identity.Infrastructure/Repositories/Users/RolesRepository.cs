#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories.Users;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories.Users
{
    public class RolesRepository : GenericRepositoryAsync<RoleEntity>, IRolesRepository
    {
        public RolesRepository(FoundersPCUsersContext context) : base(context) { }

        public override async Task<IEnumerable<RoleEntity>> GetAllAsync() =>
            await Context.Set<RoleEntity>()
                         .Include(role => role.Users)
                         .ToListAsync();
    }
}