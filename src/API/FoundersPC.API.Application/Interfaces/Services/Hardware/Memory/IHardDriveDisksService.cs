#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.Memory
{
    public interface IHardDriveDisksService : IPaginateableService<HardDriveDiskReadDto>
    {
        Task<IEnumerable<HardDriveDiskReadDto>> GetAllHardDiskDrivesAsync();

        Task<HardDriveDiskReadDto> GetHardDiskDriveByIdAsync(int hddId);

        Task<bool> CreateHardDriveDiskAsync(HardDriveDiskInsertDto hardDriveDisk);

        Task<bool> UpdateHardDriveDiskAsync(int id, HardDriveDiskUpdateDto hardDriveDisk);

        Task<bool> DeleteHardDriveDiskAsync(int id);
    }
}