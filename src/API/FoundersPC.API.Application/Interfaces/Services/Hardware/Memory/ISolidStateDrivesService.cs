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
    public interface ISolidStateDrivesService : IPaginateableService<SolidStateDriveReadDto>
    {
        /// <summary>
        ///     Return an enumeration of all <see cref="SolidStateDriveReadDto"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SolidStateDriveReadDto>> GetAllSolidStateDrivesAsync();

        Task<SolidStateDriveReadDto> GetSolidStateDriveByIdAsync(int ssdId);

        Task<bool> CreateSolidStateDriveAsync(SolidStateDriveInsertDto solidStateDrive);

        Task<bool> UpdateSolidStateDriveAsync(int id, SolidStateDriveUpdateDto solidStateDrive);

        Task<bool> DeleteSolidStateDriveAsync(int id);
    }
}