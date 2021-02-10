#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.CPU
{
	public class CPUsRepository : RepositoryBase<Models.Hardware.Processor.CPU>, ICPUsRepository
	{
		/// <inheritdoc />
		public CPUsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of ICPUsRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.Processor.CPU>> GetAllCPUsAsync() => await GetAll()
			                                                                   .Include(cpu => cpu.Producer)
			                                                                   .Include(cpu => cpu.Core)
			                                                                   .ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.Processor.CPU> GetCPUByIdAsync(int cpuId) =>
			await FindBy(cpu => cpu.Id == cpuId)
			      .Include(cpu => cpu.Producer)
			      .Include(cpu => cpu.Core).FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateCPU(Models.Hardware.Processor.CPU cpu) => await Task.Run(() => Create(cpu));
		
		/// <inheritdoc />
		public async Task UpdateCPU(Models.Hardware.Processor.CPU cpu) => await Task.Run(() => Update(cpu));
		
		/// <inheritdoc />
		public async Task DeleteCPU(Models.Hardware.Processor.CPU cpu) => await Task.Run(() => Delete(cpu));
		
		#endregion
	}
}