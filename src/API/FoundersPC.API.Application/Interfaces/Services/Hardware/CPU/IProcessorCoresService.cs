#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.CPU
{
    /// <summary>
    ///     Interface for decoration of database logic with entities
    /// </summary>
    public interface IProcessorCoresService : IPaginateableService<ProcessorCoreReadDto>
    {
        /// <summary>
        ///     Return an enumeration of all <see cref="ProcessorCoreReadDto"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProcessorCoreReadDto>> GetAllProcessorCoresAsync();

        Task<ProcessorCoreReadDto> GetProcessorCoreByIdAsync(int cpuCoreId);

        Task<bool> CreateProcessorCoreAsync(ProcessorCoreInsertDto cpuCore);

        Task<bool> UpdateProcessorCoreAsync(int id, ProcessorCoreUpdateDto cpuCore);

        Task<bool> DeleteProcessorCoreAsync(int id);
    }
}