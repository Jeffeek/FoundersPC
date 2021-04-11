#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.CPU;
using FoundersPC.API.Domain.Entities.Hardware.Processor;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.CPU
{
    public class ProcessorCoresRepository : GenericRepositoryAsync<ProcessorCore>, IProcessorCoresRepositoryAsync
    {
        /// <inheritdoc/>
        public ProcessorCoresRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IProcessorCoresRepositoryAsync

        /// <inheritdoc/>
        public override async Task<IEnumerable<ProcessorCore>> GetAllAsync()
        {
            return await Context.Set<ProcessorCore>()
                                .Include(processorCore =>
                                             processorCore.Processors)
                                .ToListAsync();
        }

        #endregion

        #region Implementation of IPaginateableRepository<ProcessorCore>

        /// <inheritdoc/>
        public async Task<IEnumerable<ProcessorCore>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Processors)
                  .ToListAsync();

        #endregion
    }
}