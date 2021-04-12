#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Hardware.Memory;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.Memory
{
    public class SSDsRepository : GenericRepositoryAsync<SSD>, ISSDsRepositoryAsync
    {
        /// <inheritdoc/>
        public SSDsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<SSD>

        /// <inheritdoc/>
        public async Task<IEnumerable<SSD>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .ToListAsync();

        #endregion

        #region Implementation of ISSDsRepositoryAsync

        public override async Task<SSD> GetByIdAsync(int id)
        {
            var ssd = await Context.Set<SSD>()
                                   .FindAsync(id);

            if (ssd is null)
                return null;

            await Context.Entry(ssd)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            return ssd;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<SSD>> GetAllAsync()
        {
            return await Context.Set<SSD>()
                                .Include(ssd => ssd.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}