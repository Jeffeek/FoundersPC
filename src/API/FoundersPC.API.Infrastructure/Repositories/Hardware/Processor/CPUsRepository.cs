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
    public class CPUsRepository : GenericRepositoryAsync<CPU>, ICPUsRepositoryAsync, IPaginateableRepository<CPU>
    {
        /// <inheritdoc/>
        public CPUsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<CPU>

        /// <inheritdoc/>
        public async Task<IEnumerable<CPU>>
            GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .Include(x => x.Core)
                  .ToListAsync();

        #endregion

        #region Implementation of ICPUsRepositoryAsync

        public override async Task<CPU> GetByIdAsync(int id)
        {
            var cpu = await Context.Set<CPU>()
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
        public override async Task<IEnumerable<CPU>> GetAllAsync()
        {
            return await Context
                         .Set<CPU>()
                         .Include(cpu => cpu.Producer)
                         .Include(cpu => cpu.Core)
                         .ToListAsync();
        }

        #endregion
    }
}