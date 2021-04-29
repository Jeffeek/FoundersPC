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
    public interface IProcessorsService : IPaginateableService<ProcessorReadDto>
    {
        /// <summary>
        ///     Return an enumeration of all <see cref="ProcessorReadDto"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProcessorReadDto>> GetAllProcessorsAsync();

        Task<ProcessorReadDto> GetProcessorByIdAsync(int cpuId);

        Task<bool> CreateProcessorAsync(ProcessorInsertDto processor);

        Task<bool> UpdateProcessorAsync(int id, ProcessorUpdateDto processor);

        Task<bool> DeleteProcessorAsync(int id);
    }
}