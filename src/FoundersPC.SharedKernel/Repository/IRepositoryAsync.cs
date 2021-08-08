#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.SharedKernel.Repository
{
    /// <summary>
    ///     Interface for all repositories in system
    /// </summary>
    /// <typeparam name="T">class</typeparam>
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> AddAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);

        Task<bool> DeleteAsync(T entity);

        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<bool> AnyAsync(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<int> CountAsync();
    }
}