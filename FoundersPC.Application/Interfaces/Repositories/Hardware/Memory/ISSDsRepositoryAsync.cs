#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware.Memory;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Hardware.Memory
{
	public interface ISSDsRepositoryAsync
	{
		Task<SSD> AddAsync(SSD ssd);

		Task UpdateAsync(SSD ssd);

		Task DeleteAsync(SSD ssd);

		Task<SSD> GetByIdAsync(int id);

		Task<IQueryable<SSD>> GetAllAsync();
	}
}