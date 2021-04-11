#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware
{
    public class MotherboardsRepository : GenericRepositoryAsync<Motherboard>, IMotherboardsRepositoryAsync
    {
        /// <inheritdoc/>
        public MotherboardsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<Motherboard>

        /// <inheritdoc/>
        public async Task<IEnumerable<Motherboard>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .ToListAsync();

        #endregion

        #region Implementation of IMotherboardsRepositoryAsync

        public override async Task<Motherboard> GetByIdAsync(int id)
        {
            var motherboard = await Context.Set<Motherboard>()
                                           .FindAsync(id);

            if (motherboard is null) return null;

            await Context.Entry(motherboard)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            return motherboard;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Motherboard>> GetAllAsync()
        {
            return await Context.Set<Motherboard>()
                                .Include(motherboard => motherboard.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}