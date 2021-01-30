#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.SSD
{
	public interface ISSDsRepository : IRepository<Models.Hardware.Memory.SSD>
	{
		Task<IEnumerable<Models.Hardware.Memory.SSD>> GetAllCPUAsync();
		Task<Models.Hardware.Memory.SSD> GetCPUByIdAsync(int ssdId);
		Task CreateSSD(Models.Hardware.Memory.SSD ssd);
		Task UpdateSSD(Models.Hardware.Memory.SSD ssd);
		Task DeleteSSD(Models.Hardware.Memory.SSD ssd);
	}
}