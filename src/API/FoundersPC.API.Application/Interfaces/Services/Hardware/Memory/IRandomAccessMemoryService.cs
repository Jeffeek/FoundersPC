#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.Memory
{
    public interface IRandomAccessMemoryService : IPaginateableService<RandomAccessMemoryReadDto>
    {
        Task<IEnumerable<RandomAccessMemoryReadDto>> GetAllRandomAccessMemoryAsync();

        Task<RandomAccessMemoryReadDto> GetRandomAccessMemoryByIdAsync(int ramId);

        Task<bool> CreateRandomAccessMemoryAsync(RandomAccessMemoryInsertDto randomAccessMemory);

        Task<bool> UpdateRandomAccessMemoryAsync(int id, RandomAccessMemoryUpdateDto randomAccessMemory);

        Task<bool> DeleteRandomAccessMemoryAsync(int id);
    }
}