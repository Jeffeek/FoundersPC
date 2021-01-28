using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.DTO;

namespace FoundersPC.Core.Requests.Producers
{
    public interface IProducerRequest
    {
	    Task<IEnumerable<ProducerDto>> GetAllProducersAsync();
	    Task<ProducerDto> GetProducerByIdAsync(int producerId);
	    IEnumerable<ProducerDto> GetAll();
	    void CreateProducer(ProducerDto producer);
	    void UpdateProducer(ProducerDto producer);
	    void DeleteProducer(ProducerDto producer);
	}
}
