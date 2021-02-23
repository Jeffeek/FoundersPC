#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.ApplicationShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.GPU
{
    public class GPUsRepository : GenericRepositoryAsync<Domain.Entities.Hardware.VideoCard.GPU>, IGPUsRepositoryAsync
    {
        /// <inheritdoc />
        public GPUsRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IGPUsRepositoryAsync

        /// <inheritdoc />
        public override async Task<IEnumerable<Domain.Entities.Hardware.VideoCard.GPU>> GetAllAsync()
        {
            return await Context
                         .Set<Domain.Entities.Hardware.VideoCard.GPU>()
                         .Include(gpu => gpu.Producer)
                         .Include(gpu => gpu.Core)
                         .ToListAsync();
        }

        #endregion
    }
}