#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Domain.Entities.Hardware.Memory;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.ApplicationShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.Memory
{
    public class RAMsRepository : GenericRepositoryAsync<RAM>, IRAMsRepositoryAsync
    {
        /// <inheritdoc />
        public RAMsRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IRAMsRepositoryAsync

        public override async Task<RAM> GetByIdAsync(int id)
        {
            var ram = await Context.Set<RAM>().FindAsync(id);

            if (ram is null) return null;

            await Context.Entry(ram).Reference<Producer>(x => x.Producer).LoadAsync();

            return ram;
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<RAM>> GetAllAsync()
        {
            return await Context.Set<RAM>()
                                .Include(ram => ram.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}