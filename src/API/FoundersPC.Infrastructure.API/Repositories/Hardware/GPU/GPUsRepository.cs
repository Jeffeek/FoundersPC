#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.ApplicationShared;
using FoundersPC.Infrastructure.API.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.API.Repositories.Hardware.GPU
{
    public class GPUsRepository : GenericRepositoryAsync<Domain.Entities.Hardware.VideoCard.GPU>, IGPUsRepositoryAsync
    {
        /// <inheritdoc />
        public GPUsRepository(FoundersPCDbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IGPUsRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<Domain.Entities.Hardware.VideoCard.GPU>> GetAllAsync()
        {
            return await _context
                         .Set<Domain.Entities.Hardware.VideoCard.GPU>()
                         .Include(gpu => gpu.Producer)
                         .Include(gpu => gpu.Core)
                         .ToListAsync();
        }

        #endregion
    }
}