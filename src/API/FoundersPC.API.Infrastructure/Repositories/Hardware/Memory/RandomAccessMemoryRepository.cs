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
    public class RandomAccessMemoryRepository : GenericRepositoryAsync<RandomAccessMemoryEntity>, IRandomAccessMemoryRepositoryAsync
    {
        /// <inheritdoc/>
        public RandomAccessMemoryRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<RandomAccessMemoryEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<RandomAccessMemoryEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.ProducerEntity)
                  .ToListAsync();

        #endregion

        #region Implementation of IRandomAccessMemoryRepositoryAsync

        public override async Task<RandomAccessMemoryEntity> GetByIdAsync(int id)
        {
            var ram = await Context.Set<RandomAccessMemoryEntity>()
                                   .FindAsync(id);

            if (ram is null)
                return null;

            await Context.Entry(ram)
                         .Reference(x => x.ProducerEntity)
                         .LoadAsync();

            return ram;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<RandomAccessMemoryEntity>> GetAllAsync()
        {
            return await Context.Set<RandomAccessMemoryEntity>()
                                .Include(ram => ram.ProducerEntity)
                                .ToListAsync();
        }

        #endregion
    }
}