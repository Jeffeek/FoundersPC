#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Application.Interfaces.Services.Hardware.GPU
{
    public interface IGPUService
    {
        Task<IEnumerable<GPUReadDto>> GetAllGPUsAsync();

        Task<GPUReadDto> GetGPUByIdAsync(int gpuId);

        Task<bool> CreateGPU(GPUInsertDto gpu);

        Task<bool> UpdateGPU(int id, GPUUpdateDto gpu);

        Task<bool> DeleteGPU(int id);
    }
}