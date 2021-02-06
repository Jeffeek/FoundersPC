#region Using derectives

using System.Linq;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Hardware.GPU
{
	public interface IGPUsRepositoryAsync
	{
		Task<Domain.Entities.Hardware.VideoCard.GPU> AddAsync(Domain.Entities.Hardware.VideoCard.GPU gpu);

		Task UpdateAsync(Domain.Entities.Hardware.VideoCard.GPU gpu);

		Task DeleteAsync(Domain.Entities.Hardware.VideoCard.GPU gpu);

		Task<Domain.Entities.Hardware.VideoCard.GPU> GetByIdAsync(int id);

		Task<IQueryable<Domain.Entities.Hardware.VideoCard.GPU>> GetAllAsync();
	}
}