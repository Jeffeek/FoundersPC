#region Using derectives

using System.Collections.Generic;
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