#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.ApplicationShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware
{
    public class MotherboardsRepository : GenericRepositoryAsync<Motherboard>, IMotherboardsRepositoryAsync
    {
        /// <inheritdoc />
        public MotherboardsRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IMotherboardsRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<Motherboard>> GetAllAsync()
        {
            return await Context.Set<Motherboard>()
                                .Include(motherboard => motherboard.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}