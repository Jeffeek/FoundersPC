#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories;
using FoundersPC.API.Domain.Entities;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware
{
    public class MotherboardsRepository : GenericRepositoryAsync<MotherboardEntity>,
                                          IMotherboardsRepositoryAsync
    {
        /// <inheritdoc/>
        public MotherboardsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<MotherboardEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<MotherboardEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.ProducerEntity)
                  .ToListAsync();

        #endregion

        #region Implementation of IMotherboardsRepositoryAsync

        public override async Task<MotherboardEntity> GetByIdAsync(int id)
        {
            var motherboard = await Context.Set<MotherboardEntity>()
                                           .FindAsync(id);

            if (motherboard is null)
                return null;

            await Context.Entry(motherboard)
                         .Reference(x => x.ProducerEntity)
                         .LoadAsync();

            return motherboard;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<MotherboardEntity>> GetAllAsync()
        {
            return await Context.Set<MotherboardEntity>()
                                .Include(motherboard => motherboard.ProducerEntity)
                                .ToListAsync();
        }

        #endregion
    }
}