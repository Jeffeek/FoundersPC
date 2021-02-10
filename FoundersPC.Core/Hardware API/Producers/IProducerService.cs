using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.DTO;

namespace FoundersPC.Core.Hardware_API.Producers
{
    public interface IProducerService
    {
	    Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync();
	    Task<ProducerReadDto> GetProducerByIdAsync(int producerId);
	    IEnumerable<ProducerReadDto> GetAllProducers();
	    Task<bool> CreateProducer(ProducerInsertDto producerRead);
	    Task<bool> UpdateProducer(int id, ProducerUpdateDto producerRead);
	    Task<bool> DeleteProducer(int id);
	}
}
