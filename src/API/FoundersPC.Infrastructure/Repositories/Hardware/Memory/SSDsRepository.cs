#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.Domain.Entities.Hardware.Memory;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware.Memory
{
    public class SSDsRepository : GenericRepositoryAsync<SSD>, ISSDsRepositoryAsync
    {
        /// <inheritdoc />
        public SSDsRepository(DbContext repositoryContext) : base(repositoryContext) { }

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