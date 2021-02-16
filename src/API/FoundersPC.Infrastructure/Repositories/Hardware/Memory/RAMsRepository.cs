#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.Domain.Entities.Hardware.Memory;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware.Memory
{
    public class RAMsRepository : GenericRepositoryAsync<RAM>, IRAMsRepositoryAsync
    {
        /// <inheritdoc />
        public RAMsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IRAMsRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<RAM>> GetAllAsync()
        {
            return await _context.Set<RAM>()
                                 .Include(ram => ram.Producer)
                                 .ToListAsync();
        }

        #endregion
    }
}