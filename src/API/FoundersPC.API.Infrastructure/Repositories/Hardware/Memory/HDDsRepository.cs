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
    public class HDDsRepository : GenericRepositoryAsync<HDD>, IHDDsRepositoryAsync, IPaginateableRepository<HDD>
    {
        /// <inheritdoc/>
        public HDDsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<HDD>

        /// <inheritdoc/>
        public async Task<IEnumerable<HDD>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .ToListAsync();

        #endregion

        #region Implementation of IHDDsRepositoryAsync

        public override async Task<HDD> GetByIdAsync(int id)
        {
            var hdd = await Context.Set<HDD>()
                                   .FindAsync(id);

            if (hdd is null)
                return null;

            await Context.Entry(hdd)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            return hdd;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<HDD>> GetAllAsync()
        {
            return await Context.Set<HDD>()
                                .Include(hdd => hdd.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}