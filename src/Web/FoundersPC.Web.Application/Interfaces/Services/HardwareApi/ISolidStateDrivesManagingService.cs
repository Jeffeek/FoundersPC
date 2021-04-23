#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface ISolidStateDrivesManagingService
    {
        Task<IEnumerable<SolidStateDriveReadDto>> GetAllSolidStateDrivesAsync(string managerToken);

        Task<SolidStateDriveReadDto> GetSolidStateDriveByIdAsync(int id, string managerToken);

        Task<bool> UpdateSolidStateDriveAsync(int id, SolidStateDriveUpdateDto solidStateDrive, string managerToken);

        Task<bool> DeleteSolidStateDriveAsync(int solidStateDriveId, string managerToken);

        Task<bool> CreateSolidStateDriveAsync(SolidStateDriveInsertDto solidStateDrive, string managerToken);

        Task<IPaginationResponse<SolidStateDriveReadDto>> GetPaginateableSolidStateDrivesAsync(int pageNumber,
                                                                                               int pageSize,
                                                                                               string managerToken);
    }
}