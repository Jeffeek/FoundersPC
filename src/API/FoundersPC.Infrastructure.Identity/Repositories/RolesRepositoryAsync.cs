#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Users;
using FoundersPC.ApplicationShared;
using FoundersPC.Domain.Entities.Users;
using FoundersPC.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Identity.Repositories
{
    public class RolesRepositoryAsync : GenericRepositoryAsync<Role>, IRolesRepositoryAsync
    {
        public RolesRepositoryAsync(FoundersPCDbIdentityUsersContext context) : base(context) { }

        public async Task<IEnumerable<Role>> GetAllAsync() => await _context.Set<Role>().ToListAsync();
    }
}