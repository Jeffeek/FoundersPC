#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.RepositoryShared.Repository
{
    public abstract class GenericRepositoryAsync<T> where T : class, IEquatable<T>
    {
        protected readonly DbContext Context;

        protected GenericRepositoryAsync(DbContext context) => Context = context;

        public virtual async Task<T> AddAsync(T entity)
        {
            var result =  await Context.Set<T>().AddAsync(entity);

            return result.Entity;
        }

        // override to include ref data
        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await Context.Set<T>().FindAsync(id);

            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            await Context.Set<T>().LoadAsync();

            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<bool> AnyAsync(T entity) => await Context.Set<T>().AnyAsync(x => x.Equals(entity));

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) =>
            await Context.Set<T>().AnyAsync(predicate);

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            var entry = Context.Entry(entity);

            if (entry == null) return false;

            await Task.Run(() => Context.Entry(entity).State = EntityState.Modified);

            return true;
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            var result = await Task.Run(() => Context.Set<T>().Remove(entity));

            return result != null;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null) return false;

            return await DeleteAsync(entity);
        }

        [Obsolete]
        public virtual async Task<IQueryable<T>> GetAllAsQueryable()
        {
            await Context.Set<T>().LoadAsync();

            return Context.Set<T>().AsQueryable();
        }
    }
}