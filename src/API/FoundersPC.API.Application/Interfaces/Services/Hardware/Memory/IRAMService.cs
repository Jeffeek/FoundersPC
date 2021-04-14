#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.Memory
{
    public interface IRAMService : IPaginateableService<RAMReadDto>
    {
        Task<IEnumerable<RAMReadDto>> GetAllRAMsAsync();

        Task<RAMReadDto> GetRAMByIdAsync(int ramId);

        Task<bool> CreateRAMAsync(RAMInsertDto ram);

        Task<bool> UpdateRAMAsync(int id, RAMUpdateDto ram);

        Task<bool> DeleteRAMAsync(int id);
    }
}