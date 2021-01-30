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
	    void CreateCPU(CPUReadDto cpu);
	    void UpdateCPU(CPUReadDto cpu);
	    void DeleteCPU(CPUReadDto cpu);
	}
}
