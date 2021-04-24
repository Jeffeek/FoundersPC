#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware
{
    /// <summary>
    ///     Interface for decoration of database logic with entities
    /// </summary>
    public interface IMotherboardsService : IPaginateableService<MotherboardReadDto>
    {
        /// <summary>
        ///     Return an enumeration of all <see cref="MotherboardReadDto"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MotherboardReadDto>> GetAllMotherboardsAsync();

        Task<MotherboardReadDto> GetMotherboardByIdAsync(int motherboardId);

        Task<bool> CreateMotherboardAsync(MotherboardInsertDto motherboard);

        Task<bool> UpdateMotherboardAsync(int id, MotherboardUpdateDto motherboard);

        Task<bool> DeleteMotherboardAsync(int id);
    }
}