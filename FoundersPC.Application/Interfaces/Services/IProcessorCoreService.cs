#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Application.Interfaces.Services
{
	public interface IProcessorCoreService
	{
		Task<IEnumerable<ProcessorCoreReadDto>> GetAllProcessorCoresAsync();
		Task<ProcessorCoreReadDto> GetProcessorCoreByIdAsync(int cpuId);
		Task<bool> CreateProcessorCore(ProcessorCoreInsertDto cpu);
		Task<bool> UpdateProcessorCore(int id, ProcessorCoreUpdateDto cpu);
		Task<bool> DeleteProcessorCore(int id);
	}
}