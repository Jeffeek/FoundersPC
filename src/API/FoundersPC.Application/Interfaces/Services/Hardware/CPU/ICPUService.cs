#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Application.Interfaces.Services.Hardware.CPU
{
    public interface ICPUService
    {
        Task<IEnumerable<CPUReadDto>> GetAllCPUsAsync();

        Task<CPUReadDto> GetCPUByIdAsync(int cpuId);

        Task<bool> CreateCPU(CPUInsertDto cpu);

        Task<bool> UpdateCPU(int id, CPUUpdateDto cpu);

        Task<bool> DeleteCPU(int id);
    }
}