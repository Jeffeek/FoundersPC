#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Repositories.Logs;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories.Logs
{
    public class AccessTokensLogsRepository : GenericRepositoryAsync<AccessTokenLog>, IAccessTokensLogsRepository
    {
        public AccessTokensLogsRepository(DbContext context) : base(context) { }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="navigationPropertyPath"/>
        ///     is <see langword="null"/>.
        /// </exception>

        #endregion

        public override async Task<IEnumerable<AccessTokenLog>> GetAllAsync() =>
            await Context.Set<AccessTokenLog>()
                         .Include(log => log.AccessTokenEntity)
                         .ToListAsync();

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="predicate"/> is
        ///     <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<IEnumerable<AccessTokenLog>> GetUsagesBetweenAsync(DateTime start, DateTime finish) =>
            await Context.Set<AccessTokenLog>()
                         .Where(log => log.RequestDateTime >= start && log.RequestDateTime <= finish)
                         .ToListAsync();

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="predicate"/> is
        ///     <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<IEnumerable<AccessTokenLog>> GetUsagesInAsync(DateTime date) =>
            await Context.Set<AccessTokenLog>()
                         .Where(log => log.RequestDateTime.Year == date.Year
                                       && log.RequestDateTime.Month == date.Month
                                       && log.RequestDateTime.Day == date.Day)
                         .ToListAsync();

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="predicate"/> is
        ///     <see langword="null"/>.
        /// </exception>

        #endregion

        public Task<AccessTokenLog> GetLastTokenUsageAsync(int apiAccessTokenId) =>
            Context.Set<AccessTokenLog>()
                   .Where(x => x.ApiAccessUsersTokenId == apiAccessTokenId)
                   .OrderBy(x => x.Id)
                   .LastOrDefaultAsync();

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="navigationPropertyPath"/>
        ///     is <see langword="null"/>.
        /// </exception>

        #endregion

        public Task<AccessTokenLog> GetLastTokenUsageAsync(string apiAccessToken) =>
            Context.Set<AccessTokenLog>()
                   .Include(x => x.AccessTokenEntity)
                   .Where(x => x.AccessTokenEntity.HashedToken == apiAccessToken)
                   .OrderBy(x => x.Id)
                   .LastOrDefaultAsync();

        #region MyRegion

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="navigationPropertyPath"/>
        ///     is <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<IEnumerable<AccessTokenLog>> GetUserTokenUsagesByUserIdAsync(int userId) =>
            await Context.Set<AccessTokenLog>()
                         .Include(x => x.AccessTokenEntity)
                         .Where(x => x.AccessTokenEntity.UserId == userId)
                         .ToListAsync();

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="navigationPropertyPath"/>
        ///     is <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<IEnumerable<AccessTokenLog>> GetUserTokenUsagesByUserEmailAsync(string userEmail) =>
            await Context.Set<AccessTokenLog>()
                         .Include(x => x.AccessTokenEntity)
                         .ThenInclude(x => x.User)
                         .Where(x => x.AccessTokenEntity.User.Email == userEmail)
                         .ToListAsync();

        #region Implementation of IPaginateableRepository<AccessTokenLog>

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentOutOfRangeException">pageNumber or pageSize was below or equal to 0.</exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     source or keySelector is
        ///     <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<IEnumerable<AccessTokenLog>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await Context.Set<AccessTokenLog>()
                         .Paginate(pageNumber, pageSize)
                         .Include(x => x.AccessTokenEntity)
                         .ToListAsync();

        #endregion
    }
}