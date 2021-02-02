#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.ProcessorCores
{
	public interface IProcessorCoresRepository : IRepository<Models.Hardware.Processor.ProcessorCore>
	{
		Task<IEnumerable<Models.Hardware.Processor.ProcessorCore>> GetAllProcessorCoresAsync();
		Task<Models.Hardware.Processor.ProcessorCore> GetProcessorCoreByIdAsync(int processorCoreId);
		Task CreateProcessorCore(Models.Hardware.Processor.ProcessorCore processorCore);
		Task UpdateProcessorCore(Models.Hardware.Processor.ProcessorCore processorCore);
		Task DeleteProcessorCore(Models.Hardware.Processor.ProcessorCore processorCore);
	}
}