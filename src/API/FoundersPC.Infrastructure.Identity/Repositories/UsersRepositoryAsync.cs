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
    public class UsersRepositoryAsync : GenericRepositoryAsync<User>, IUsersRepositoryAsync
    {
        public UsersRepositoryAsync(FoundersPCDbIdentityUsersContext context) : base(context) { }

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _context.Set<User>()
                          .Include(user => user.Role)
                          .ToListAsync();
    }
}