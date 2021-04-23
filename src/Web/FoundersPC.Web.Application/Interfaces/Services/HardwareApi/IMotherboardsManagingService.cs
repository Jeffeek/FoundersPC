#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface IMotherboardsManagingService
    {
        Task<IEnumerable<MotherboardReadDto>> GetAllMotherboardsAsync(string managerToken);

        Task<MotherboardReadDto> GetMotherboardByIdAsync(int id, string managerToken);

        Task<bool> UpdateMotherboardAsync(int id, MotherboardUpdateDto motherboard, string managerToken);

        Task<bool> DeleteMotherboardAsync(int motherboardId, string managerToken);

        Task<bool> CreateMotherboardAsync(MotherboardInsertDto motherboard, string managerToken);

        Task<IPaginationResponse<MotherboardReadDto>> GetPaginateableMotherboardsAsync(int pageNumber,
                                                                                       int pageSize,
                                                                                       string managerToken);
    }
}