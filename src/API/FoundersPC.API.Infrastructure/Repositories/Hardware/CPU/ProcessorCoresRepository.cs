#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.CPU;
using FoundersPC.API.Domain.Entities.Hardware.Processor;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.CPU
{
    public class ProcessorCoresRepository : GenericRepositoryAsync<ProcessorCore>, IProcessorCoresRepositoryAsync
    {
        /// <inheritdoc/>
        public ProcessorCoresRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

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
    }
}