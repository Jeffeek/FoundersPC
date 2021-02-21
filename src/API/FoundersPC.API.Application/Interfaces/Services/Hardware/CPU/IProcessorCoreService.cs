#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.CPU
{
    public interface IProcessorCoreService
    {
        Task<IEnumerable<ProcessorCoreReadDto>> GetAllProcessorCoresAsync();

        Task<ProcessorCoreReadDto> GetProcessorCoreByIdAsync(int cpuCoreId);

        Task<bool> CreateProcessorCore(ProcessorCoreInsertDto cpuCore);

        Task<bool> UpdateProcessorCore(int id, ProcessorCoreUpdateDto cpuCore);

        Task<bool> DeleteProcessorCore(int id);
    }
}