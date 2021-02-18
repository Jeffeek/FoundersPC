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
    public class CasesRepository : GenericRepositoryAsync<Case>, ICasesRepositoryAsync
    {
        /// <inheritdoc />
        public CasesRepository(FoundersPCDbContext context) : base(context) { }

        #region Implementation of ICasesRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<Case>> GetAllAsync()
        {
            return await _context.Set<Case>()
                                 .Include(@case => @case.Producer)
                                 .ToListAsync();
        }

        #endregion
    }
}