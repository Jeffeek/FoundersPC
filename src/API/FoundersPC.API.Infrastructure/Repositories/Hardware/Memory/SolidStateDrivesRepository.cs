#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Memory;
using FoundersPC.API.Domain.Entities.Hardware.Memory;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.Memory
{
    public class SolidStateDrivesRepository : GenericRepositoryAsync<SolidStateDrive>, ISolidStateDrivesRepositoryAsync
    {
        /// <inheritdoc/>
        public SolidStateDrivesRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<SolidStateDriveEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<SolidStateDrive>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .ToListAsync();

        #endregion

        #region Implementation of ISolidStateDrivesRepositoryAsync

        public override async Task<SolidStateDrive> GetByIdAsync(int id)
        {
            var ssd = await Context.Set<SolidStateDrive>()
                                   .FindAsync(id);

            if (ssd is null)
                return null;

            await Context.Entry(ssd)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            return ssd;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<SolidStateDrive>> GetAllAsync()
        {
            return await Context.Set<SolidStateDrive>()
                                .Include(ssd => ssd.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}