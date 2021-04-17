#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.Memory
{
    public interface ISolidStateDrivesService : IPaginateableService<SolidStateDriveReadDto>
    {
        Task<IEnumerable<SolidStateDriveReadDto>> GetAllSolidStateDrivesAsync();

        Task<SolidStateDriveReadDto> GetSolidStateDriveByIdAsync(int ssdId);

        Task<bool> CreateSolidStateDriveAsync(SolidStateDriveInsertDto solidStateDrive);

        Task<bool> UpdateSolidStateDriveAsync(int id, SolidStateDriveUpdateDto solidStateDrive);

        Task<bool> DeleteSolidStateDriveAsync(int id);
    }
}