#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.CPU
{
	public class CPUsRepository : RepositoryBase<Models.Hardware.CPU>, ICPUsRepository
	{
		/// <inheritdoc />
		public CPUsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of ICPURepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.CPU>> GetAllCPUAsync() => await GetAll()
			.Include(x => x.Producer)
			.Include(x => x.ProcessorLineup)
			.ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.CPU> GetCPUByIdAsync(int cpuId) =>
			await FindBy(cpu => cpu.Id == cpuId)
			      .Include(x => x.Producer)
			      .Include(x => x.ProcessorLineup).FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateCPU(Models.Hardware.CPU cpu) => await Task.Run(() => Create(cpu));
		
		/// <inheritdoc />
		public async Task UpdateCPU(Models.Hardware.CPU cpu) => await Task.Run(() => Update(cpu));
		
		/// <inheritdoc />
		public async Task DeleteCPU(Models.Hardware.CPU cpu) => await Task.Run(() => Delete(cpu));
		

		#endregion
	}
}