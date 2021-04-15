#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.IdentityEntities.Identity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.RepositoryShared.Repository
{
    public abstract class GenericRepositoryAsync<T> : IRepositoryAsync<T> where T : class, IEquatable<T>, IIdentityItem
    {
        protected readonly DbContext Context;

        protected GenericRepositoryAsync(DbContext context) => Context = context;

        public virtual async Task<T> AddAsync(T entity)
        {
            var result = await Context.Set<T>()
                                      .AddAsync(entity);

            return result.Entity;
        }

        // override to include ref data
        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await Context.Set<T>()
                                      .FindAsync(id);

            return entity;
        }

        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            await Context.Set<T>()
                         .LoadAsync();

            return await Context.Set<T>()
                                .ToListAsync();
        }

        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
        public virtual Task<bool> AnyAsync(T entity) =>
            Context.Set<T>()
                   .AnyAsync(x => x.Equals(entity));

        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) =>
            Context.Set<T>()
                   .AnyAsync(predicate);

        /// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter is <see langword="null" />.</exception>
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            var entry = Context.Entry(entity);

            if (entry == null)
                return false;

            await Task.Run(() => Context.Entry(entity)
                                        .State = EntityState.Modified);

            return true;
        }

        /// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter is <see langword="null" />.</exception>
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            var result = await Task.Run(() => Context.Set<T>()
                                                     .Remove(entity));

            return result != null;
        }

        /// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter is <see langword="null" />.</exception>
        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                return false;

            return await DeleteAsync(entity);
        }

        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is <see langword="null" />.</exception>
        public virtual Task<int> CountAsync() =>
            Context.Set<T>()
                   .CountAsync();

        protected IQueryable<T> GetPaginateableInternal(int pageNumber = 1, int pageSize = 10) =>
            Context.Set<T>()
                   .Paginate(pageNumber, pageSize);
    }
}