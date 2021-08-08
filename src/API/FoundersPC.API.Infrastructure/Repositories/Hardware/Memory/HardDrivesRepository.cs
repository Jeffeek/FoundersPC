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
    /// <inheritdoc/>
    public class HardDrivesRepository : GenericRepositoryAsync<HardDriveDisk>, IHardDrivesRepositoryAsync
    {
        /// <inheritdoc/>
        public HardDrivesRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<HardDriveDiskEntity>

        #region Docs

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentOutOfRangeException">pageNumber or pageSize was below or equal to 0.</exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="navigationPropertyPath"/>
        ///     is <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<IEnumerable<HardDriveDisk>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .ToListAsync();

        #endregion

        #region Implementation of IHardDrivesRepositoryAsync

        public override async Task<HardDriveDisk> GetByIdAsync(int id)
        {
            var hdd = await Context.Set<HardDriveDisk>()
                                   .FindAsync(id);

            if (hdd is null)
                return null;

            await Context.Entry(hdd)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            return hdd;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<HardDriveDisk>> GetAllAsync()
        {
            return await Context.Set<HardDriveDisk>()
                                .Include(hdd => hdd.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}