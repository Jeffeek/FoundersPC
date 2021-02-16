#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware;
using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware
{
    public class PowerSuppliersRepository : GenericRepositoryAsync<PowerSupply>, IPowerSuppliersRepositoryAsync
    {
        /// <inheritdoc />
        public PowerSuppliersRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPowerSuppliersRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<PowerSupply>> GetAllAsync()
        {
            return await _context.Set<PowerSupply>()
                                 .Include(powerSupply => powerSupply.Producer)
                                 .ToListAsync();
        }

        #endregion
    }
}