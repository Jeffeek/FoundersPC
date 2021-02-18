#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.CPU;
using FoundersPC.ApplicationShared;
using FoundersPC.Domain.Entities.Hardware.Processor;
using FoundersPC.Infrastructure.API.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.API.Repositories.Hardware.CPU
{
    public class ProcessorCoresRepository : GenericRepositoryAsync<ProcessorCore>, IProcessorCoresRepositoryAsync
    {
        /// <inheritdoc />
        public ProcessorCoresRepository(FoundersPCDbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IProcessorCoresRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<ProcessorCore>> GetAllAsync()
        {
            return await _context.Set<ProcessorCore>()
                                 .Include(processorCore =>
                                              processorCore.Processors)
                                 .ToListAsync();
        }

        #endregion
    }
}