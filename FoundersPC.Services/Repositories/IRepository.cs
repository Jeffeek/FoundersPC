using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundersPC.Services.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
	    Task<TEntity> GetAsync(int? id);
	    Task<IEnumerable<TEntity>> GetAllAsync();
	    Task AddAsync(TEntity entity);
	    Task RemoveAsync(TEntity entity);
	    Task UpdateAsync(TEntity entity);
    }
}
