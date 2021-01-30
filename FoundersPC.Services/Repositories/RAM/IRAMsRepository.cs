#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.RAM
{
	public interface IRAMsRepository : IRepository<Models.Hardware.Memory.RAM>
	{
		Task<IEnumerable<Models.Hardware.Memory.RAM>> GetAllRAMsAsync();
		Task<Models.Hardware.Memory.RAM> GetRAMByIdAsync(int ramId);
		Task CreateRAM(Models.Hardware.Memory.RAM ram);
		Task UpdateRAM(Models.Hardware.Memory.RAM ram);
		Task DeleteRAM(Models.Hardware.Memory.RAM ram);
	}
}