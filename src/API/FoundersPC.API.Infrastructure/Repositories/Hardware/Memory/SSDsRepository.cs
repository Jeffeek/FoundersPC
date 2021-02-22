#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Hardware.Memory;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.ApplicationShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.Memory
{
    public class SSDsRepository : GenericRepositoryAsync<SSD>, ISSDsRepositoryAsync
    {
        /// <inheritdoc />
        public SSDsRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of ISSDsRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<SSD>> GetAllAsync()
        {
            return await Context.Set<SSD>()
                                .Include(ssd => ssd.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}