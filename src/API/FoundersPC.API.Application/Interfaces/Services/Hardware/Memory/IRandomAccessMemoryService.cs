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
    public interface IRandomAccessMemoryService : IPaginateableService<RandomAccessMemoryReadDto>
    {
        /// <summary>
        ///     Return an enumeration of all <see cref="RandomAccessMemoryReadDto"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RandomAccessMemoryReadDto>> GetAllRandomAccessMemoryAsync();

        Task<RandomAccessMemoryReadDto> GetRandomAccessMemoryByIdAsync(int ramId);

        Task<bool> CreateRandomAccessMemoryAsync(RandomAccessMemoryInsertDto randomAccessMemory);

        Task<bool> UpdateRandomAccessMemoryAsync(int id, RandomAccessMemoryUpdateDto randomAccessMemory);

        Task<bool> DeleteRandomAccessMemoryAsync(int id);
    }
}