#region Using derectives

using System.Linq;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories
{
	public interface IGenericRepositoryAsync<T> where T : class
	{
		Task<T> GetByIdAsync(int id);
		Task<IQueryable<T>> GetAllAsync();
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
	}
}