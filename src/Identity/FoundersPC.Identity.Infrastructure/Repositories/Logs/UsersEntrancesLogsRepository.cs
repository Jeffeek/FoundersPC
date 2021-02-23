using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories.Logs;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Identity.Infrastructure.Repositories.Logs
{
    public class UsersEntrancesLogsRepository : GenericRepositoryAsync<UserEntranceLog>, IUsersEntrancesLogsRepository
    {
        public UsersEntrancesLogsRepository(FoundersPCUsersContext context) : base(context) { }

        public override async Task<IEnumerable<UserEntranceLog>> GetAllAsync() =>
            await Context.Set<UserEntranceLog>()
                         .Include(log => log.User)
                         .ThenInclude(user => user.Role)
                         .ToListAsync();
    }
}
