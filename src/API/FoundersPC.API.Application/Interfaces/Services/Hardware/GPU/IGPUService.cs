#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.GPU
{
    public interface IGPUService
    {
        Task<IEnumerable<GPUReadDto>> GetAllGPUsAsync();

        Task<GPUReadDto> GetGPUByIdAsync(int gpuId);

        Task<bool> CreateGPUAsync(GPUInsertDto gpu);

        Task<bool> UpdateGPUAsync(int id, GPUUpdateDto gpu);

        Task<bool> DeleteGPUAsync(int id);
    }
}