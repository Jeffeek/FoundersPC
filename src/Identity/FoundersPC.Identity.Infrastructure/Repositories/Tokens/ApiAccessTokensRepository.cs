using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories;
using FoundersPC.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Identity.Infrastructure.Repositories.Tokens
{
    public class ApiAccessTokensRepository : GenericRepositoryAsync<ApiAccessToken>, IApiAccessTokensRepository
    {
        public ApiAccessTokensRepository(DbContext context) : base(context) { }

        public Task<IEnumerable<ApiAccessToken>> GetAllAsync() => throw new NotImplementedException();
    }
}
