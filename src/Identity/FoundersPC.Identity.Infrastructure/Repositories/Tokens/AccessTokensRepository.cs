#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Repositories.Tokens;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories.Tokens
{
    public class AccessTokensRepository : GenericRepositoryAsync<AccessTokenEntity>,
                                          IAccessTokensRepository
    {
        public AccessTokensRepository(DbContext context) : base(context) { }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="navigationPropertyPath"/>
        ///     is <see langword="null"/>.
        /// </exception>

        #endregion

        public override async Task<IEnumerable<AccessTokenEntity>> GetAllAsync() =>
            await Context.Set<AccessTokenEntity>()
                         .Include(token => token.User)
                         .ThenInclude(user => user.Role)
                         .ToListAsync();

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="predicate"/> is
        ///     <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<AccessTokenEntity> GetByTokenAsync(string token)
        {
            var tokenEntity = await Context.Set<AccessTokenEntity>()
                                           .FirstOrDefaultAsync(x => x.HashedToken == token);

            if (tokenEntity is null) return null;

            await Context.Entry(tokenEntity)
                         .Collection(x => x.UsagesLogs)
                         .LoadAsync();

            return tokenEntity;
        }

        #region Overrides of GenericRepositoryAsync<AccessTokenEntity>

        #region Docs

        /// <inheritdoc/>

        #endregion

        public override async Task<AccessTokenEntity> GetByIdAsync(int id)
        {
            var tokenEntity = await Context.Set<AccessTokenEntity>()
                                           .FindAsync(id);

            if (tokenEntity is null) return null;

            await Context.Entry(tokenEntity)
                         .Collection(x => x.UsagesLogs)
                         .LoadAsync();

            return tokenEntity;
        }

        #endregion

        public async Task<IEnumerable<AccessTokenEntity>> GetAllUserTokensAsync(int userId)
        {
            var user = await Context.Set<UserEntity>()
                                    .FindAsync(userId);

            if (user is null)
                return null;

            await Context.Entry(user)
                         .Collection(x => x.Tokens)
                         .LoadAsync();

            return user.Tokens;
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="predicate"/> is
        ///     <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<IEnumerable<AccessTokenEntity>> GetAllUserTokensAsync(string userEmail)
        {
            if (userEmail is null)
                return null;

            var user = await Context.Set<UserEntity>()
                                    .FirstOrDefaultAsync(x => x.Email == userEmail);

            if (user is null)
                return null;

            await Context.Entry(user)
                         .Collection(x => x.Tokens)
                         .LoadAsync();

            return user.Tokens;
        }

        #region Implementation of IPaginateableRepository<AccessTokenEntity>

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentOutOfRangeException">pageNumber or pageSize was below or equal to 0.</exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     source or keySelector is
        ///     <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<IEnumerable<AccessTokenEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await Context.Set<AccessTokenEntity>()
                         .Paginate(pageNumber, pageSize)
                         .Include(x => x.User)
                         .ToListAsync();

        #endregion
    }
}