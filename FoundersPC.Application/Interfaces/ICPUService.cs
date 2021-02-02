using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoundersPC.Application.Interfaces
{
    public interface ICPUService
    {
	    Task<IEnumerable<CPUReadDto>> GetAllCPUsAsync();
	    Task<CPUReadDto> GetCPUByIdAsync(int cpuId);
	    Task<bool> CreateCPU(CPUInsertDto cpu);
	    Task<bool> UpdateCPU(int id, CPUUpdateDto cpu);
	    Task<bool> DeleteCPU(int id);
	}
}
