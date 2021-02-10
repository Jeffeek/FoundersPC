#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.HDD
{
	public interface IHDDsRepository : IRepository<Models.Hardware.Memory.HDD>
	{
		Task<IEnumerable<Models.Hardware.Memory.HDD>> GetAllHDDsAsync();
		Task<Models.Hardware.Memory.HDD> GetHDDByIdAsync(int cpuId);
		Task CreateHDD(Models.Hardware.Memory.HDD cpu);
		Task UpdateHDD(Models.Hardware.Memory.HDD cpu);
		Task DeleteHDD(Models.Hardware.Memory.HDD cpu);
	}
}