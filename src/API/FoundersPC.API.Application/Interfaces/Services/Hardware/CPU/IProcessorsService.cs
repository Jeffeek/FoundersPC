#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.CPU
{
    public interface IProcessorsService : IPaginateableService<ProcessorReadDto>
    {
        Task<IEnumerable<ProcessorReadDto>> GetAllProcessorsAsync();

        Task<ProcessorReadDto> GetProcessorByIdAsync(int cpuId);

        Task<bool> CreateProcessorAsync(ProcessorInsertDto processor);

        Task<bool> UpdateProcessorAsync(int id, ProcessorUpdateDto processor);

        Task<bool> DeleteProcessorAsync(int id);
    }
}