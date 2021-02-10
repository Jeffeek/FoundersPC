#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.PowerSupply
{
	public interface IPowerSuppliersRepository : IRepository<Models.Hardware.PowerSupply>
	{
		Task<IEnumerable<Models.Hardware.PowerSupply>> GetAllPowerSuppliersAsync();
		Task<Models.Hardware.PowerSupply> GetPowerSupplyByIdAsync(int powerSupplyId);
		Task CreatePowerSupply(Models.Hardware.PowerSupply powerSupply);
		Task UpdatePowerSupply(Models.Hardware.PowerSupply powerSupply);
		Task DeletePowerSupply(Models.Hardware.PowerSupply powerSupply);
	}
}