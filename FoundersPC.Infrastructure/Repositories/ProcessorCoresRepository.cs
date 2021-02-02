#region Using derectives

using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware.Processor;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public class ProcessorCoresRepository : GenericRepositoryAsync<ProcessorCore>, IProcessorCoresRepositoryAsync
	{
		/// <inheritdoc />
		public ProcessorCoresRepository(DbContext repositoryContext) : base(repositoryContext) { }
	}
}