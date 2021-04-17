#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Repositories.Users;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories.Users
{
    public class UsersRepository : GenericRepositoryAsync<UserEntity>, IUsersRepository
    {
        public UsersRepository(DbContext context) : base(context) { }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="navigationPropertyPath"/>
        ///     is <see langword="null"/>.
        /// </exception>

        #endregion

        public override async Task<IEnumerable<UserEntity>> GetAllAsync() =>
            await Context.Set<UserEntity>()
                         .Include(user => user.Role)
                         .ToListAsync();

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="predicate"/> is
        ///     <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<UserEntity> GetUserByAsync(Expression<Func<UserEntity, bool>> predicate)
        {
            var user = await Context.Set<UserEntity>()
                                    .FirstOrDefaultAsync(predicate);

            if (user is null)
                return null;

            await Context.Entry(user)
                         .Reference(x => x.Role)
                         .LoadAsync();

            await Context.Entry(user)
                         .Collection(x => x.Tokens)
                         .LoadAsync();

            return user;
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="predicate"/> is
        ///     <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<UserEntity> GetUserByEmailAsync(string userEmail)
        {
            var user = await Context.Set<UserEntity>()
                                    .FirstOrDefaultAsync(x => x.Email == userEmail);

            if (user is null)
                return null;

            await Context.Entry(user)
                         .Reference(x => x.Role)
                         .LoadAsync();

            await Context.Entry(user)
                         .Collection(x => x.Tokens)
                         .LoadAsync();

            return user;
        }

        public override async Task<UserEntity> GetByIdAsync(int id)
        {
            var user = await Context.Set<UserEntity>()
                                    .FindAsync(id);

            if (user is null)
                return null;

            await Context.Entry(user)
                         .Reference(x => x.Role)
                         .LoadAsync();

            await Context.Entry(user)
                         .Collection(x => x.Tokens)
                         .LoadAsync();

            return user;
        }

        #region Implementation of IPaginateableRepository<UserEntity>

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="navigationPropertyPath"/>
        ///     is <see langword="null"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">pageNumber or pageSize was below or equal to 0.</exception>

        #endregion

        public async Task<IEnumerable<UserEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Role)
                  .ToListAsync();

        #endregion
    }
}