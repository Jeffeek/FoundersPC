using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories;
using FoundersPC.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Identity.Infrastructure.Repositories.Logs
{
    public class UsersEntrancesLogsRepository : GenericRepositoryAsync<UserEntranceLog>, IUsersEntrancesLogsRepository
    {
        public UsersEntrancesLogsRepository(DbContext context) : base(context) { }

        public Task<IEnumerable<UserEntranceLog>> GetAllAsync() => throw new NotImplementedException();
    }
}
