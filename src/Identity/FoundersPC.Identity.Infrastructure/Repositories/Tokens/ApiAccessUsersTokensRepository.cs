using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories.Tokens;
using FoundersPC.Identity.Domain.Entities.Tokens;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Identity.Infrastructure.Repositories.Tokens
{
    public class ApiAccessUsersTokensRepository : GenericRepositoryAsync<ApiAccessUserToken>, IApiAccessUsersTokensRepository
    {
        public ApiAccessUsersTokensRepository(DbContext context) : base(context) { }

        public Task<IEnumerable<ApiAccessUserToken>> GetAllAsync() => throw new NotImplementedException();
    }
}
