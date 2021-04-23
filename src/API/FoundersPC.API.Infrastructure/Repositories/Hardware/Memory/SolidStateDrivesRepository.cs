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
    public class SolidStateDrivesRepository : GenericRepositoryAsync<SolidStateDriveEntity>, ISolidStateDrivesRepositoryAsync
    {
        /// <inheritdoc/>
        public SolidStateDrivesRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<SolidStateDriveEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<SolidStateDriveEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.ProducerEntity)
                  .ToListAsync();

        #endregion

        #region Implementation of ISolidStateDrivesRepositoryAsync

        public override async Task<SolidStateDriveEntity> GetByIdAsync(int id)
        {
            var ssd = await Context.Set<SolidStateDriveEntity>()
                                   .FindAsync(id);

            if (ssd is null)
                return null;

            await Context.Entry(ssd)
                         .Reference(x => x.ProducerEntity)
                         .LoadAsync();

            return ssd;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<SolidStateDriveEntity>> GetAllAsync()
        {
            return await Context.Set<SolidStateDriveEntity>()
                                .Include(ssd => ssd.ProducerEntity)
                                .ToListAsync();
        }

        #endregion
    }
}