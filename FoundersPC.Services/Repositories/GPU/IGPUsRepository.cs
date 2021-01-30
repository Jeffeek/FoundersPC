#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.GPU
{
	public interface IGPUsRepository : IRepository<Models.Hardware.GPU>
	{
		Task<IEnumerable<Models.Hardware.GPU>> GetAllGPUsAsync();
		Task<Models.Hardware.GPU> GetGPUByIdAsync(int gpuId);
		Task CreateGPU(Models.Hardware.GPU gpu);
		Task UpdateGPU(Models.Hardware.GPU gpu);
		Task DeleteGPU(Models.Hardware.GPU gpu);
	}
}