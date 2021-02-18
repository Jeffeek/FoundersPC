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
    public class HDDsRepository : GenericRepositoryAsync<HDD>, IHDDsRepositoryAsync
    {
        /// <inheritdoc />
        public HDDsRepository(FoundersPCDbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IHDDsRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<HDD>> GetAllAsync()
        {
            return await _context.Set<HDD>()
                                 .Include(hdd => hdd.Producer)
                                 .ToListAsync();
        }

        #endregion
    }
}