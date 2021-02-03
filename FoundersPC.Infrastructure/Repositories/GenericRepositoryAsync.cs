#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public abstract class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
	{
		private readonly DbContext _context;

		protected GenericRepositoryAsync(DbContext context) => _context = context;

		#region Implementation of IGenericRepositoryAsync<T>

		/// <inheritdoc />
		public async Task<T> AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return entity;
		}

		/// <inheritdoc />
		public async Task UpdateAsync(T entity) =>
			await Task.Run(() => _context.Entry(entity).State = EntityState.Modified);

		/// <inheritdoc />
		public async Task DeleteAsync(T entity) => await Task.Run(() => _context.Set<T>().Remove(entity));

		/// <inheritdoc />
		public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

		/// <inheritdoc />
		public virtual async Task<IQueryable<T>> GetAllAsync() => await Task.Run(() => _context.Set<T>().AsQueryable());

		#endregion
	}
}