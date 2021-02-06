#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.CPU;
using FoundersPC.Domain.Entities.Hardware.Processor;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware.CPU
{
	public class ProcessorCoresRepository : GenericRepositoryAsync<ProcessorCore>, IProcessorCoresRepositoryAsync
	{
		/// <inheritdoc />
		public ProcessorCoresRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IProcessorCoresRepositoryAsync

		/// <inheritdoc />
		public async Task<ProcessorCore> GetByIdAsync(int id) =>
			await (await GetAllAsync()).FirstOrDefaultAsync(processorCore => processorCore.Id == id);

		/// <inheritdoc />
		public async Task<IQueryable<ProcessorCore>> GetAllAsync() =>
			await Task.Run(() => _context.Set<ProcessorCore>().Include(processorCore => processorCore.Processors));

		#endregion
	}
}