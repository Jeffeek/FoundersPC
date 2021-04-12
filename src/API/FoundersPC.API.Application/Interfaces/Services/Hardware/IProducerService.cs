#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware
{
    public interface IProducerService : IPaginateableService<ProducerReadDto>
    {
        Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync();

        Task<ProducerReadDto> GetProducerByIdAsync(int producerId);

        Task<bool> CreateProducerAsync(ProducerInsertDto producer);

        Task<bool> UpdateProducerAsync(int id, ProducerUpdateDto producer);

        Task<bool> DeleteProducerAsync(int id);
    }
}