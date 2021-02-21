#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories;
using FoundersPC.Identity.Domain.Entities;
using FoundersPC.Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories
{
    public class RolesRepository : GenericRepositoryAsync<RoleEntity>, IRolesRepository
    {
        public RolesRepository(FoundersPCUsersContext context) : base(context) { }

        public async Task<IEnumerable<RoleEntity>> GetAllAsync() =>
            await Context.Set<RoleEntity>()
                         .Include(role => role.Users)
                         .ToListAsync();
    }
}