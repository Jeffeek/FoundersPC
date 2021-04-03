#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.Memory
{
    public interface IHDDService
    {
        Task<IEnumerable<HDDReadDto>> GetAllHDDsAsync();

        Task<HDDReadDto> GetHDDByIdAsync(int hddId);

        Task<bool> CreateHDDAsync(HDDInsertDto hdd);

        Task<bool> UpdateHDDAsync(int id, HDDUpdateDto hdd);

        Task<bool> DeleteHDDAsync(int id);
    }
}