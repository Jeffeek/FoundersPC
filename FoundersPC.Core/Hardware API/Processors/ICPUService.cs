using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.DTO;

namespace FoundersPC.Core.Hardware_API.Processors
{
    public interface ICPUService
    {
	    Task<IEnumerable<CPUReadDto>> GetAllProducersAsync();
	    Task<CPUReadDto> GetProducerByIdAsync(int producerId);
	    IEnumerable<CPUReadDto> GetAll();
	    void CreateProducer(CPUReadDto producer);
	    void UpdateProducer(CPUReadDto producer);
	    void DeleteProducer(CPUReadDto producer);
	}
}
