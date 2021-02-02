#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.ProcessorCores
{
	public class ProcessorCoresRepository : RepositoryBase<Models.Hardware.Processor.ProcessorCore>, IProcessorCoresRepository
	{
		/// <inheritdoc />
		public ProcessorCoresRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IProcessorLineupsRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.Processor.ProcessorCore>> GetAllProcessorCoresAsync() => await GetAll().ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.Processor.ProcessorCore> GetProcessorCoreByIdAsync(int processorCoreId) =>
			await FindBy(processorLineup => processorLineup.Id == processorCoreId).FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateProcessorCore(Models.Hardware.Processor.ProcessorCore processorCore) => await Task.Run(() => Create(processorCore));
		
		/// <inheritdoc />
		public async Task UpdateProcessorCore(Models.Hardware.Processor.ProcessorCore processorCore) => await Task.Run(() => Update(processorCore));

		/// <inheritdoc />
		public async Task DeleteProcessorCore(Models.Hardware.Processor.ProcessorCore processorCore) => await Task.Run(() => Delete(processorCore));

		#endregion
	}
}