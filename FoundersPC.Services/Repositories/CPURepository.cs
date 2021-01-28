#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories
{
	public class CPURepository : RepositoryBase<CPU>, ICPURepository
	{
		/// <inheritdoc />
		public CPURepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of ICPURepository

		/// <inheritdoc />
		public async Task<IEnumerable<CPU>> GetAllCPUAsync() => await GetAll().Include(x => x.Producer).ToListAsync();

		/// <inheritdoc />
		public async Task<CPU> GetCPUByIdAsync(int cpuId) =>
			await FindBy(cpu => cpu.Id == cpuId).Include(x => x.Producer).FirstOrDefaultAsync();

		/// <inheritdoc />
		public void CreateCPU(CPU cpu)
		{
			Create(cpu);
		}

		/// <inheritdoc />
		public void UpdateCPU(CPU cpu)
		{
			Update(cpu);
		}

		/// <inheritdoc />
		public void DeleteCPU(CPU cpu)
		{
			Delete(cpu);
		}

		#endregion
	}
}