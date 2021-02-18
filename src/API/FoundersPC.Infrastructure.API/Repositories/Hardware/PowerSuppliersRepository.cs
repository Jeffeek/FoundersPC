#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware;
using FoundersPC.ApplicationShared;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Infrastructure.API.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.API.Repositories.Hardware
{
    public class PowerSuppliersRepository : GenericRepositoryAsync<PowerSupply>, IPowerSuppliersRepositoryAsync
    {
        /// <inheritdoc />
        public PowerSuppliersRepository(FoundersPCDbContext repositoryContext) : base(repositoryContext) { }

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