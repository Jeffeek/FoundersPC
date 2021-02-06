#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware.Memory;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Hardware.Memory
{
	public interface IHDDsRepositoryAsync
	{
		Task<HDD> AddAsync(HDD hdd);

		Task UpdateAsync(HDD hdd);

		Task DeleteAsync(HDD hdd);

		Task<HDD> GetByIdAsync(int id);

		Task<IQueryable<HDD>> GetAllAsync();
	}
}