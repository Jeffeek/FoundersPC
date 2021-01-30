#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.Motherboard
{
	public interface IMotherboardsRepository : IRepository<Models.Hardware.Motherboard>
	{
		Task<IEnumerable<Models.Hardware.Motherboard>> GetAllMotherboardsAsync();
		Task<Models.Hardware.Motherboard> GetMotherboardByIdAsync(int cpuId);
		Task CreateMotherboard(Models.Hardware.Motherboard motherboard);
		Task UpdateMotherboard(Models.Hardware.Motherboard motherboard);
		Task DeleteMotherboard(Models.Hardware.Motherboard motherboard);
	}
}