#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.CPU
{
	public interface ICPUsRepository : IRepository<Models.Hardware.CPU>
	{
		Task<IEnumerable<Models.Hardware.CPU>> GetAllCPUAsync();
		Task<Models.Hardware.CPU> GetCPUByIdAsync(int cpuId);
		Task CreateCPU(Models.Hardware.CPU cpu);
		Task UpdateCPU(Models.Hardware.CPU cpu);
		Task DeleteCPU(Models.Hardware.CPU cpu);
	}
}