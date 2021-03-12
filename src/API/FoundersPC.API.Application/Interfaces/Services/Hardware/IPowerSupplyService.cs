#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware
{
	public interface IPowerSupplyService
	{
		Task<IEnumerable<PowerSupplyReadDto>> GetAllPowerSuppliesAsync();

		Task<PowerSupplyReadDto> GetPowerSupplyByIdAsync(int powerSupplyId);

		Task<bool> CreatePowerSupplyAsync(PowerSupplyInsertDto powerSupply);

		Task<bool> UpdatePowerSupplyAsync(int id, PowerSupplyUpdateDto powerSupply);

		Task<bool> DeletePowerSupplyAsync(int id);
	}
}