#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.Memory
{
    public interface IHDDService : IPaginateableService<HDDReadDto>
    {
        Task<IEnumerable<HDDReadDto>> GetAllHDDsAsync();

        Task<HDDReadDto> GetHDDByIdAsync(int hddId);

        Task<bool> CreateHDDAsync(HDDInsertDto hdd);

        Task<bool> UpdateHDDAsync(int id, HDDUpdateDto hdd);

        Task<bool> DeleteHDDAsync(int id);
    }
}