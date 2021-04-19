#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface IHardDriveDisksManagingService
    {
        Task<IEnumerable<HardDriveDiskReadDto>> GetAllHardDriveDisksAsync(string managerToken);

        Task<HardDriveDiskReadDto> GetHardDriveDiskByIdAsync(int id, string managerToken);

        Task<bool> UpdateHardDriveDiskAsync(int id, HardDriveDiskUpdateDto hardDriveDisk, string managerToken);

        Task<bool> DeleteHardDriveDiskAsync(int hardDriveDiskId, string managerToken);

        Task<bool> CreateHardDriveDiskAsync(HardDriveDiskInsertDto hardDriveDisk, string managerToken);

        Task<IPaginationResponse<CaseReadDto>> GetPaginateableHardDriveDisksAsync(int pageNumber,
                                                                                  int pageSize,
                                                                                  string managerToken);
    }
}