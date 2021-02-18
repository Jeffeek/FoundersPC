#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.ApplicationShared;
using FoundersPC.Domain.Entities.Hardware.Memory;
using FoundersPC.Infrastructure.API.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.API.Repositories.Hardware.Memory
{
    public class SSDsRepository : GenericRepositoryAsync<SSD>, ISSDsRepositoryAsync
    {
        /// <inheritdoc />
        public SSDsRepository(FoundersPCDbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of ISSDsRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<SSD>> GetAllAsync()
        {
            return await _context.Set<SSD>()
                                 .Include(ssd => ssd.Producer)
                                 .ToListAsync();
        }

        #endregion
    }
}