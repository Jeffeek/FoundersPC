#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.Memory
{
    /// <summary>
    ///     Interface for decoration of database logic with entities
    /// </summary>
    public interface IHardDriveDisksService : IPaginateableService<HardDriveDiskReadDto>
    {
        /// <summary>
        ///     Return an enumeration of all <see cref="HardDriveDiskReadDto"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<HardDriveDiskReadDto>> GetAllHardDiskDrivesAsync();

        Task<HardDriveDiskReadDto> GetHardDiskDriveByIdAsync(int hddId);

        Task<bool> CreateHardDriveDiskAsync(HardDriveDiskInsertDto hardDriveDisk);

        Task<bool> UpdateHardDriveDiskAsync(int id, HardDriveDiskUpdateDto hardDriveDisk);

        Task<bool> DeleteHardDriveDiskAsync(int id);
    }
}