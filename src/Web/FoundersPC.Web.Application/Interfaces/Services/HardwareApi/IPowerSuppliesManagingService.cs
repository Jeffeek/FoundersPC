#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface IPowerSuppliesManagingService
    {
        Task<IEnumerable<PowerSupplyReadDto>> GetAllPowerSuppliesAsync(string managerToken);

        Task<PowerSupplyReadDto> GetPowerSupplyByIdAsync(int id, string managerToken);

        Task<bool> UpdatePowerSupplyAsync(int id, PowerSupplyUpdateDto powerSupply, string managerToken);

        Task<bool> DeletePowerSupplyAsync(int powerSupplyId, string managerToken);

        Task<bool> CreatePowerSupplyAsync(PowerSupplyInsertDto powerSupply, string managerToken);

        Task<IPaginationResponse<PowerSupplyReadDto>> GetPaginateablePowerSuppliesAsync(int pageNumber,
                                                                                        int pageSize,
                                                                                        string managerToken);
    }
}