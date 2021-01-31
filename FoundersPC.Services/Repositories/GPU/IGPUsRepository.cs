#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.GPU
{
	public interface IGPUsRepository : IRepository<Models.Hardware.VideoCard.GPU>
	{
		Task<IEnumerable<Models.Hardware.VideoCard.GPU>> GetAllGPUsAsync();
		Task<Models.Hardware.VideoCard.GPU> GetGPUByIdAsync(int gpuId);
		Task CreateGPU(Models.Hardware.VideoCard.GPU gpu);
		Task UpdateGPU(Models.Hardware.VideoCard.GPU gpu);
		Task DeleteGPU(Models.Hardware.VideoCard.GPU gpu);
	}
}