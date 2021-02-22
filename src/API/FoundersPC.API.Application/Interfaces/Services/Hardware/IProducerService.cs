#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware
{
    public interface IProducerService
    {
        Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync();

        Task<ProducerReadDto> GetProducerByIdAsync(int producerId);

        Task<bool> CreateProducer(ProducerInsertDto producer);

        Task<bool> UpdateProducer(int id, ProducerUpdateDto producer);

        Task<bool> DeleteProducer(int id);
    }
}