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
    public class HardDrivesRepository : GenericRepositoryAsync<HardDriveDiskEntity>, IHardDrivesRepositoryAsync
    {
        /// <inheritdoc/>
        public HardDrivesRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<HardDriveDiskEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<HardDriveDiskEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.ProducerEntity)
                  .ToListAsync();

        #endregion

        #region Implementation of IHardDrivesRepositoryAsync

        public override async Task<HardDriveDiskEntity> GetByIdAsync(int id)
        {
            var hdd = await Context.Set<HardDriveDiskEntity>()
                                   .FindAsync(id);

            if (hdd is null)
                return null;

            await Context.Entry(hdd)
                         .Reference(x => x.ProducerEntity)
                         .LoadAsync();

            return hdd;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<HardDriveDiskEntity>> GetAllAsync()
        {
            return await Context.Set<HardDriveDiskEntity>()
                                .Include(hdd => hdd.ProducerEntity)
                                .ToListAsync();
        }

        #endregion
    }
}