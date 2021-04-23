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
    public class PowerSuppliersRepository : GenericRepositoryAsync<PowerSupplyEntity>,
                                            IPowerSuppliersRepositoryAsync
    {
        /// <inheritdoc/>
        public PowerSuppliersRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<PowerSupplyEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<PowerSupplyEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.ProducerEntity)
                  .ToListAsync();

        #endregion

        #region Implementation of IPowerSuppliersRepositoryAsync

        public override async Task<PowerSupplyEntity> GetByIdAsync(int id)
        {
            var powerSupply = await Context.Set<PowerSupplyEntity>()
                                           .FindAsync(id);

            if (powerSupply is null)
                return null;

            await Context.Entry(powerSupply)
                         .Reference(x => x.ProducerEntity)
                         .LoadAsync();

            return powerSupply;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<PowerSupplyEntity>> GetAllAsync()
        {
            return await Context.Set<PowerSupplyEntity>()
                                .Include(powerSupply => powerSupply.ProducerEntity)
                                .ToListAsync();
        }

        #endregion
    }
}