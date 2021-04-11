#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.GPU
{
    public class GPUsRepository : GenericRepositoryAsync<Domain.Entities.Hardware.VideoCard.GPU>, IGPUsRepositoryAsync
    {
        /// <inheritdoc/>
        public GPUsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<GPU>

        /// <inheritdoc/>
        public async Task<IEnumerable<Domain.Entities.Hardware.VideoCard.GPU>>
            GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .Include(x => x.Core)
                  .ToListAsync();

        #endregion

        #region Implementation of IGPUsRepositoryAsync

        public override async Task<Domain.Entities.Hardware.VideoCard.GPU> GetByIdAsync(int id)
        {
            var gpu = await Context.Set<Domain.Entities.Hardware.VideoCard.GPU>()
                                   .FindAsync(id);

            if (gpu is null) return null;

            await Context.Entry(gpu)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            await Context.Entry(gpu)
                         .Reference(x => x.Core)
                         .LoadAsync();

            return gpu;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Domain.Entities.Hardware.VideoCard.GPU>> GetAllAsync()
        {
            return await Context
                         .Set<Domain.Entities.Hardware.VideoCard.GPU>()
                         .Include(gpu => gpu.Producer)
                         .Include(gpu => gpu.Core)
                         .ToListAsync();
        }

        #endregion
    }
}