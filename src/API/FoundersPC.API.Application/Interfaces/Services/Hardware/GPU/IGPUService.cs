#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.GPU
{
    public interface IGPUService : IPaginateableService<GPUReadDto>
    {
        Task<IEnumerable<GPUReadDto>> GetAllGPUsAsync();

        Task<GPUReadDto> GetGPUByIdAsync(int gpuId);

        Task<bool> CreateGPUAsync(GPUInsertDto gpu);

        Task<bool> UpdateGPUAsync(int id, GPUUpdateDto gpu);

        Task<bool> DeleteGPUAsync(int id);
    }
}