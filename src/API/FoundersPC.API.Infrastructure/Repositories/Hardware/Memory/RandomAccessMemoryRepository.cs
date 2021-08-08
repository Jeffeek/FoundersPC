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
    public class RandomAccessMemoryRepository : GenericRepositoryAsync<RandomAccessMemory>, IRandomAccessMemoryRepositoryAsync
    {
        /// <inheritdoc/>
        public RandomAccessMemoryRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<RandomAccessMemoryEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<RandomAccessMemory>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .ToListAsync();

        #endregion

        #region Implementation of IRandomAccessMemoryRepositoryAsync

        public override async Task<RandomAccessMemory> GetByIdAsync(int id)
        {
            var ram = await Context.Set<RandomAccessMemory>()
                                   .FindAsync(id);

            if (ram is null)
                return null;

            await Context.Entry(ram)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            return ram;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<RandomAccessMemory>> GetAllAsync()
        {
            return await Context.Set<RandomAccessMemory>()
                                .Include(ram => ram.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}