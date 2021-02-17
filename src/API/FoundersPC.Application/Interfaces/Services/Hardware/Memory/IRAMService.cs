#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Application.Interfaces.Services.Hardware.Memory
{
    public interface IRAMService
    {
        Task<IEnumerable<RAMReadDto>> GetAllRAMsAsync();

        Task<RAMReadDto> GetRAMByIdAsync(int ramId);

        Task<bool> CreateRAM(RAMInsertDto ram);

        Task<bool> UpdateRAM(int id, RAMUpdateDto ram);

        Task<bool> DeleteRAM(int id);
    }
}