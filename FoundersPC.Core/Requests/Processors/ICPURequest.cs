using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.DTO;

namespace FoundersPC.Core.Requests.Processors
{
    public interface ICPURequest
    {
	    Task<IEnumerable<CPUDto>> GetAllProducersAsync();
	    Task<CPUDto> GetProducerByIdAsync(int producerId);
	    IEnumerable<CPUDto> GetAll();
	    void CreateProducer(CPUDto producer);
	    void UpdateProducer(CPUDto producer);
	    void DeleteProducer(CPUDto producer);
	}
}
