#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Hardware.Memory;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.Memory
{
    public class SSDsRepository : GenericRepositoryAsync<SSD>, ISSDsRepositoryAsync
    {
        /// <inheritdoc/>
        public SSDsRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of ISSDsRepositoryAsync

        public override async Task<SSD> GetByIdAsync(int id)
        {
            var ssd = await Context.Set<SSD>().FindAsync(id);

            if (ssd is null) return null;

            await Context.Entry(ssd).Reference(x => x.Producer).LoadAsync();

            return ssd;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<SSD>> GetAllAsync()
        {
            return await Context.Set<SSD>()
                                .Include(ssd => ssd.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}