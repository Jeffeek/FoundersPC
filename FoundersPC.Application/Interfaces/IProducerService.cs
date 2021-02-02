using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoundersPC.Application.Interfaces
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
