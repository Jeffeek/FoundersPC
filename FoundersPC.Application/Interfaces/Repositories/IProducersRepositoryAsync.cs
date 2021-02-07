#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories
{
	public interface IProducersRepositoryAsync
	{
		Task<Producer> AddAsync(Producer producer);

		Task UpdateAsync(Producer producer);

		Task DeleteAsync(Producer producer);

		Task<Producer> GetByIdAsync(int id);

		Task<IQueryable<Producer>> GetAllAsync(bool includeEquipment);

		Task<bool> AnyAsync(Producer producer);
	}
}