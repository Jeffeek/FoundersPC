#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.Memory
{
    public interface ISSDService : IPaginateableService<SSDReadDto>
    {
        Task<IEnumerable<SSDReadDto>> GetAllSSDsAsync();

        Task<SSDReadDto> GetSSDByIdAsync(int ssdId);

        Task<bool> CreateSSDAsync(SSDInsertDto ssd);

        Task<bool> UpdateSSDAsync(int id, SSDUpdateDto ssd);

        Task<bool> DeleteSSDAsync(int id);
    }
}