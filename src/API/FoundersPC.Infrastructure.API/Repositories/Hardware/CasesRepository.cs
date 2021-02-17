#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware;
using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.API.Repositories.Hardware
{
    public class CasesRepository : GenericRepositoryAsync<Case>, ICasesRepositoryAsync
    {
        /// <inheritdoc />
        public CasesRepository(DbContext context) : base(context) { }

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