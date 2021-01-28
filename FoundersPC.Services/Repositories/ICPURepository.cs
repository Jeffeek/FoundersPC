#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Models;

#endregion

namespace FoundersPC.Services.Repositories
{
	public interface ICPURepository : IRepository<CPU>
	{
		Task<IEnumerable<CPU>> GetAllCPUAsync();
		Task<CPU> GetCPUByIdAsync(int cpuId);
		void CreateCPU(CPU cpu);
		void UpdateCPU(CPU cpu);
		void DeleteCPU(CPU cpu);
	}
}