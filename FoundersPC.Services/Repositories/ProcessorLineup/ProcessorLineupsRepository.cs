#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.ProcessorLineup
{
	public class ProcessorLineupsRepository : RepositoryBase<Models.Hardware.ProcessorLineup>, IProcessorLineupsRepository
	{
		/// <inheritdoc />
		public ProcessorLineupsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IProducersRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.ProcessorLineup>> GetAllProcessorLineupAsync() => await GetAll().ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.ProcessorLineup> GetProcessorLineupByIdAsync(int processorLineupId) =>
			await FindBy(processorLineup => processorLineup.Id == processorLineupId).FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateProcessorLineup(Models.Hardware.ProcessorLineup processorLineup) => await Task.Run(() => Create(processorLineup));
		
		/// <inheritdoc />
		public async Task UpdateProcessorLineup(Models.Hardware.ProcessorLineup processorLineup) => await Task.Run(() => Update(processorLineup));

		/// <inheritdoc />
		public async Task DeleteProcessorLineup(Models.Hardware.ProcessorLineup processorLineup) => await Task.Run(() => Delete(processorLineup));

		#endregion
	}
}