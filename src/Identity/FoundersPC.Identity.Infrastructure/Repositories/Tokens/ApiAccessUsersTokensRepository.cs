#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories.Tokens;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories.Tokens
{
    public class ApiAccessUsersTokensRepository : GenericRepositoryAsync<ApiAccessUserToken>, IApiAccessUsersTokensRepository
    {
        public ApiAccessUsersTokensRepository(FoundersPCUsersContext context) : base(context) { }

        public override async Task<IEnumerable<ApiAccessUserToken>> GetAllAsync() =>
            await Context.Set<ApiAccessUserToken>()
                         .Include(token => token.User)
                         .ThenInclude(user => user.Role)
                         .ToListAsync();

        public async Task<ApiAccessUserToken> GetByTokenAsync(string token) =>
            await Context.Set<ApiAccessUserToken>()
                         .SingleOrDefaultAsync(x => x.HashedToken == token);

        public async Task<IEnumerable<ApiAccessUserToken>> GetAllUserTokens(int userId)
        {
            var user = await Context.Set<UserEntity>().FindAsync(userId);

            if (user is null) return null;

            await Context.Entry(user).Collection(x => x.Tokens).LoadAsync();

            return user.Tokens;
        }

        public async Task<IEnumerable<ApiAccessUserToken>> GetAllUserTokens(string userEmail)
        {
            if (userEmail is null) return null;

            var user = await Context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Email == userEmail);

            if (user is null) return null;

            await Context.Entry(user).Collection(x => x.Tokens).LoadAsync();

            return user.Tokens;
        }
    }
}