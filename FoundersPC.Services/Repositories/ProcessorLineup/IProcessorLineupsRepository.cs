#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.ProcessorLineup
{
	public interface IProcessorLineupsRepository : IRepository<Models.Hardware.ProcessorLineup>
	{
		Task<IEnumerable<Models.Hardware.ProcessorLineup>> GetAllProcessorLineupAsync();
		Task<Models.Hardware.ProcessorLineup> GetProcessorLineupByIdAsync(int processorLineupId);
		Task CreateProcessorLineup(Models.Hardware.ProcessorLineup processorLineup);
		Task UpdateProcessorLineup(Models.Hardware.ProcessorLineup processorLineup);
		Task DeleteProcessorLineup(Models.Hardware.ProcessorLineup processorLineup);
	}
}