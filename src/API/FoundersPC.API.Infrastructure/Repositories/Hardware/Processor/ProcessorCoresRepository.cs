#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Processor;
using FoundersPC.API.Domain.Entities.Processor;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.Processor
{
    public class ProcessorCoresRepository : GenericRepositoryAsync<ProcessorCoreEntity>,
                                            IProcessorCoresRepositoryAsync
    {
        /// <inheritdoc/>
        public ProcessorCoresRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IProcessorCoresRepositoryAsync

        /// <inheritdoc/>
        public override async Task<IEnumerable<ProcessorCoreEntity>> GetAllAsync()
        {
            return await Context.Set<ProcessorCoreEntity>()
                                .Include(processorCore =>
                                             processorCore.Processors)
                                .ToListAsync();
        }

        #endregion

        #region Implementation of IPaginateableRepository<ProcessorCore>

        /// <inheritdoc/>
        public async Task<IEnumerable<ProcessorCoreEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Processors)
                  .ToListAsync();

        #endregion
    }
}