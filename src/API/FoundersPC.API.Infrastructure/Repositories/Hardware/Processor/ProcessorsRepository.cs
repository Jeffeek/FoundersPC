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
    public class ProcessorsRepository : GenericRepositoryAsync<ProcessorEntity>, IProcessorsRepositoryAsync
    {
        /// <inheritdoc/>
        public ProcessorsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<ProcessorEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<ProcessorEntity>>
            GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.ProducerEntity)
                  .Include(x => x.Core)
                  .ToListAsync();

        #endregion

        #region Implementation of IProcessorsRepositoryAsync

        public override async Task<ProcessorEntity> GetByIdAsync(int id)
        {
            var cpu = await Context.Set<ProcessorEntity>()
                                   .FindAsync(id);

            if (cpu is null)
                return null;

            await Context.Entry(cpu)
                         .Reference(x => x.ProducerEntity)
                         .LoadAsync();

            await Context.Entry(cpu)
                         .Reference(x => x.Core)
                         .LoadAsync();

            return cpu;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<ProcessorEntity>> GetAllAsync()
        {
            return await Context
                         .Set<ProcessorEntity>()
                         .Include(cpu => cpu.ProducerEntity)
                         .Include(cpu => cpu.Core)
                         .ToListAsync();
        }

        #endregion
    }
}