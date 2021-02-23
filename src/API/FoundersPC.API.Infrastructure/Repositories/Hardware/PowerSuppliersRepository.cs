#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.ApplicationShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware
{
    public class PowerSuppliersRepository : GenericRepositoryAsync<PowerSupply>, IPowerSuppliersRepositoryAsync
    {
        /// <inheritdoc />
        public PowerSuppliersRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPowerSuppliersRepositoryAsync

        /// <inheritdoc />
        public override async Task<IEnumerable<PowerSupply>> GetAllAsync()
        {
            return await Context.Set<PowerSupply>()
                                .Include(powerSupply => powerSupply.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}