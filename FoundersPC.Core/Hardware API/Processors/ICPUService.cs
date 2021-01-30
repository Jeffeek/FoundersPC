using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.DTO;

namespace FoundersPC.Core.Hardware_API.Processors
{
    public interface ICPUService
    {
	    Task<IEnumerable<CPUReadDto>> GetAllCPUsAsync();
	    Task<CPUReadDto> GetCPUByIdAsync(int cpuId);
	    IEnumerable<CPUReadDto> GetAllCPUs();
	    Task<bool> CreateCPU(CPUInsertDto cpu);
	    Task<bool> UpdateCPU(CPUUpdateDto cpu);
	    Task<bool> DeleteCPU(int id);
	}
}
