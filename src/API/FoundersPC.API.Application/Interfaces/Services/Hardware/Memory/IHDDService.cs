#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.Memory
{
    public interface IHDDService
    {
        Task<IEnumerable<HDDReadDto>> GetAllHDDsAsync();

        Task<HDDReadDto> GetHDDByIdAsync(int hddId);

        Task<bool> CreateHDD(HDDInsertDto hdd);

        Task<bool> UpdateHDD(int id, HDDUpdateDto hdd);

        Task<bool> DeleteHDD(int id);
    }
}