#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.VideoCard;
using FoundersPC.API.Domain.Entities.VideoCard;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.VideoCard
{
    public class GPUsRepository : GenericRepositoryAsync<GPU>,
                                  IGPUsRepositoryAsync
    {
        /// <inheritdoc/>
        public GPUsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<GPU>

        /// <inheritdoc/>
        public async Task<IEnumerable<GPU>>
            GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .Include(x => x.Core)
                  .ToListAsync();

        #endregion

        #region Implementation of IGPUsRepositoryAsync

        public override async Task<GPU> GetByIdAsync(int id)
        {
            var gpu = await Context.Set<GPU>()
                                   .FindAsync(id);

            if (gpu is null)
                return null;

            await Context.Entry(gpu)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            await Context.Entry(gpu)
                         .Reference(x => x.Core)
                         .LoadAsync();

            return gpu;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<GPU>> GetAllAsync()
        {
            return await Context
                         .Set<GPU>()
                         .Include(gpu => gpu.Producer)
                         .Include(gpu => gpu.Core)
                         .ToListAsync();
        }

        #endregion
    }
}