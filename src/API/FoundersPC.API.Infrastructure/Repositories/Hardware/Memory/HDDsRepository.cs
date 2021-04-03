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
    public class HDDsRepository : GenericRepositoryAsync<HDD>, IHDDsRepositoryAsync
    {
        /// <inheritdoc/>
        public HDDsRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IHDDsRepositoryAsync

        public override async Task<HDD> GetByIdAsync(int id)
        {
            var hdd = await Context.Set<HDD>().FindAsync(id);

            if (hdd is null) return null;

            await Context.Entry(hdd).Reference(x => x.Producer).LoadAsync();

            return hdd;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<HDD>> GetAllAsync()
        {
            return await Context.Set<HDD>()
                                .Include(hdd => hdd.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}