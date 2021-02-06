#region Using derectives

using System.Linq;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Hardware.CPU
{
	public interface ICPUsRepositoryAsync
	{
		Task<Domain.Entities.Hardware.Processor.CPU> AddAsync(Domain.Entities.Hardware.Processor.CPU cpu);

		Task UpdateAsync(Domain.Entities.Hardware.Processor.CPU cpu);

		Task DeleteAsync(Domain.Entities.Hardware.Processor.CPU cpu);

		Task<Domain.Entities.Hardware.Processor.CPU> GetByIdAsync(int id);

		Task<IQueryable<Domain.Entities.Hardware.Processor.CPU>> GetAllAsync();
	}
}