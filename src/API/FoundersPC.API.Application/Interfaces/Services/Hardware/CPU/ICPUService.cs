#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.CPU
{
    public interface ICPUService
    {
        Task<IEnumerable<CPUReadDto>> GetAllCPUsAsync();

        Task<CPUReadDto> GetCPUByIdAsync(int cpuId);

        Task<bool> CreateCPUAsync(CPUInsertDto cpu);

        Task<bool> UpdateCPUAsync(int id, CPUUpdateDto cpu);

        Task<bool> DeleteCPUAsync(int id);
    }
}