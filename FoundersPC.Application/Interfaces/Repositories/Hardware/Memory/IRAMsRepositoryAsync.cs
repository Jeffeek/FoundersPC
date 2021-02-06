#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware.Memory;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Hardware.Memory
{
	public interface IRAMsRepositoryAsync
	{
		Task<RAM> AddAsync(RAM ram);

		Task UpdateAsync(RAM ram);

		Task DeleteAsync(RAM ram);

		Task<RAM> GetByIdAsync(int id);

		Task<IQueryable<RAM>> GetAllAsync();
	}
}