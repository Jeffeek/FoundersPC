#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.ProcessorLineup
{
	public interface IProcessorLineupsRepository : IRepository<Models.Hardware.Processor.ProcessorCore>
	{
		Task<IEnumerable<Models.Hardware.Processor.ProcessorCore>> GetAllProcessorLineupAsync();
		Task<Models.Hardware.Processor.ProcessorCore> GetProcessorLineupByIdAsync(int processorLineupId);
		Task CreateProcessorLineup(Models.Hardware.Processor.ProcessorCore processorLineup);
		Task UpdateProcessorLineup(Models.Hardware.Processor.ProcessorCore processorLineup);
		Task DeleteProcessorLineup(Models.Hardware.Processor.ProcessorCore processorLineup);
	}
}