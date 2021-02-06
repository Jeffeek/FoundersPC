#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware.Processor;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Hardware.CPU
{
	public interface IProcessorCoresRepositoryAsync
	{
		Task<ProcessorCore> AddAsync(ProcessorCore processorCore);

		Task UpdateAsync(ProcessorCore processorCore);

		Task DeleteAsync(ProcessorCore processorCore);

		Task<ProcessorCore> GetByIdAsync(int id);

		Task<IQueryable<ProcessorCore>> GetAllAsync();
	}
}