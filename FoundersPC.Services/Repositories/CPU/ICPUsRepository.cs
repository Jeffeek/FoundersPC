#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.CPU
{
	public interface ICPUsRepository : IRepository<Models.Hardware.Processor.CPU>
	{
		Task<IEnumerable<Models.Hardware.Processor.CPU>> GetAllCPUsAsync();
		Task<Models.Hardware.Processor.CPU> GetCPUByIdAsync(int cpuId);
		Task CreateCPU(Models.Hardware.Processor.CPU cpu);
		Task UpdateCPU(Models.Hardware.Processor.CPU cpu);
		Task DeleteCPU(Models.Hardware.Processor.CPU cpu);
	}
}