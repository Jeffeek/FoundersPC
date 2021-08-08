#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Processor;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.Processor
{
    public class ProcessorsRepository : GenericRepositoryAsync<Domain.Entities.Hardware.Processor.Processor>, IProcessorsRepositoryAsync
    {
        /// <inheritdoc/>
        public ProcessorsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<ProcessorEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<Domain.Entities.Hardware.Processor.Processor>>
            GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .Include(x => x.Core)
                  .ToListAsync();

        #endregion

        #region Implementation of IProcessorsRepositoryAsync

        public override async Task<Domain.Entities.Hardware.Processor.Processor> GetByIdAsync(int id)
        {
            var cpu = await Context.Set<Domain.Entities.Hardware.Processor.Processor>()
                                   .FindAsync(id);

            if (cpu is null)
                return null;

            await Context.Entry(cpu)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            await Context.Entry(cpu)
                         .Reference(x => x.Core)
                         .LoadAsync();

            return cpu;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Domain.Entities.Hardware.Processor.Processor>> GetAllAsync()
        {
            return await Context
                         .Set<Domain.Entities.Hardware.Processor.Processor>()
                         .Include(cpu => cpu.Producer)
                         .Include(cpu => cpu.Core)
                         .ToListAsync();
        }

        #endregion
    }
}