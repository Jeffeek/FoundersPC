#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Hardware
{
	public interface IMotherboardsRepositoryAsync
	{
		Task<Motherboard> AddAsync(Motherboard motherboard);

		Task UpdateAsync(Motherboard motherboard);

		Task DeleteAsync(Motherboard motherboard);

		Task<Motherboard> GetByIdAsync(int id);

		Task<IQueryable<Motherboard>> GetAllAsync();
	}
}