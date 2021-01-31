#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.ProcessorLineup
{
	public class ProcessorLineupsRepository : RepositoryBase<Models.Hardware.Processor.ProcessorCore>, IProcessorLineupsRepository
	{
		/// <inheritdoc />
		public ProcessorLineupsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IProcessorLineupsRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.Processor.ProcessorCore>> GetAllProcessorLineupAsync() => await GetAll().ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.Processor.ProcessorCore> GetProcessorLineupByIdAsync(int processorLineupId) =>
			await FindBy(processorLineup => processorLineup.Id == processorLineupId).FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateProcessorLineup(Models.Hardware.Processor.ProcessorCore processorLineup) => await Task.Run(() => Create(processorLineup));
		
		/// <inheritdoc />
		public async Task UpdateProcessorLineup(Models.Hardware.Processor.ProcessorCore processorLineup) => await Task.Run(() => Update(processorLineup));

		/// <inheritdoc />
		public async Task DeleteProcessorLineup(Models.Hardware.Processor.ProcessorCore processorLineup) => await Task.Run(() => Delete(processorLineup));

		#endregion
	}
}