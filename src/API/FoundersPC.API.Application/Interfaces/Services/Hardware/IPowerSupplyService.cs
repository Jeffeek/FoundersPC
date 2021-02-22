#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware
{
    public interface IPowerSupplyService
    {
        Task<IEnumerable<PowerSupplyReadDto>> GetAllPowerSuppliersAsync();

        Task<PowerSupplyReadDto> GetPowerSupplyByIdAsync(int powerSupplyId);

        Task<bool> CreatePowerSupply(PowerSupplyInsertDto powerSupply);

        Task<bool> UpdatePowerSupply(int id, PowerSupplyUpdateDto powerSupply);

        Task<bool> DeletePowerSupply(int id);
    }
}