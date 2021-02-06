#region Using derectives

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public abstract class GenericRepositoryAsync<T> where T : class
	{
		protected readonly DbContext _context;

		protected GenericRepositoryAsync(DbContext context) => _context = context;

		public virtual async Task<T> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return entity;
		}

		public virtual async Task UpdateAsync(T entity) =>
			await Task.Run(() => _context.Entry(entity).State = EntityState.Modified);

		public virtual async Task DeleteAsync(T entity) => await Task.Run(() => _context.Set<T>().Remove(entity));
	}
}