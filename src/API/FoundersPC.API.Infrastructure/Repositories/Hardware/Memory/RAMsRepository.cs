#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Memory;
using FoundersPC.API.Domain.Entities.Memory;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.Memory
{
    public class RAMsRepository : GenericRepositoryAsync<RAM>, IRAMsRepositoryAsync, IPaginateableRepository<RAM>
    {
        /// <inheritdoc/>
        public RAMsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<RAM>

        /// <inheritdoc/>
        public async Task<IEnumerable<RAM>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .ToListAsync();

        #endregion

        #region Implementation of IRAMsRepositoryAsync

        public override async Task<RAM> GetByIdAsync(int id)
        {
            var ram = await Context.Set<RAM>()
                                   .FindAsync(id);

            if (ram is null)
                return null;

            await Context.Entry(ram)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            return ram;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<RAM>> GetAllAsync()
        {
            return await Context.Set<RAM>()
                                .Include(ram => ram.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}