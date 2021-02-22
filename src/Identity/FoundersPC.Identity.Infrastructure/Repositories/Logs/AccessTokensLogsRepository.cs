using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories;
using FoundersPC.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Identity.Infrastructure.Repositories
{
    public class AccessTokensLogsRepository : GenericRepositoryAsync<AccessTokenLog>, IAccessTokensLogsRepository
    {
        public AccessTokensLogsRepository(DbContext context) : base(context) { }

        public Task<IEnumerable<AccessTokenLog>> GetAllAsync() => throw new NotImplementedException();
    }
}
