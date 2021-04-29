#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services
{
    /// <summary>
    ///     Interface for decoration of database logic with entities
    /// </summary>
    public interface IProducersService : IPaginateableService<ProducerReadDto>
    {
        /// <summary>
        ///     Return an enumeration of all <see cref="ProducerReadDto"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync();

        Task<ProducerReadDto> GetProducerByIdAsync(int producerId);

        Task<bool> CreateProducerAsync(ProducerInsertDto producer);

        Task<bool> UpdateProducerAsync(int id, ProducerUpdateDto producer);

        Task<bool> DeleteProducerAsync(int id);
    }
}