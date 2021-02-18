#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware;
using FoundersPC.ApplicationShared;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Infrastructure.API.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.API.Repositories.Hardware
{
    public class MotherboardsRepository : GenericRepositoryAsync<Motherboard>, IMotherboardsRepositoryAsync
    {
        /// <inheritdoc />
        public MotherboardsRepository(FoundersPCDbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IMotherboardsRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<Motherboard>> GetAllAsync()
        {
            return await _context.Set<Motherboard>()
                                 .Include(motherboard => motherboard.Producer)
                                 .ToListAsync();
        }

        #endregion
    }
}