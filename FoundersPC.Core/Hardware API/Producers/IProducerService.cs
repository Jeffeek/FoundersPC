using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.DTO;

namespace FoundersPC.Core.Hardware_API.Producers
{
    public interface IProducerService
    {
	    Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync();
	    Task<ProducerReadDto> GetProducerByIdAsync(int producerId);
	    IEnumerable<ProducerReadDto> GetAll();
	    void CreateProducer(ProducerReadDto producerRead);
	    void UpdateProducer(ProducerReadDto producerRead);
	    void DeleteProducer(ProducerReadDto producerRead);
	}
}
