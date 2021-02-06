#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Hardware
{
	public interface IPowerSuppliersRepositoryAsync
	{
		Task<PowerSupply> AddAsync(PowerSupply powerSupply);

		Task UpdateAsync(PowerSupply powerSupply);

		Task DeleteAsync(PowerSupply powerSupply);

		Task<PowerSupply> GetByIdAsync(int id);

		Task<IQueryable<PowerSupply>> GetAllAsync();
	}
}