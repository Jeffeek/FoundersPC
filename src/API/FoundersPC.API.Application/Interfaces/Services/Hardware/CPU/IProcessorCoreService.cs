#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.CPU
{
    public interface IProcessorCoreService : IPaginateableService<ProcessorCoreReadDto>
    {
        Task<IEnumerable<ProcessorCoreReadDto>> GetAllProcessorCoresAsync();

        Task<ProcessorCoreReadDto> GetProcessorCoreByIdAsync(int cpuCoreId);

        Task<bool> CreateProcessorCoreAsync(ProcessorCoreInsertDto cpuCore);

        Task<bool> UpdateProcessorCoreAsync(int id, ProcessorCoreUpdateDto cpuCore);

        Task<bool> DeleteProcessorCoreAsync(int id);
    }
}