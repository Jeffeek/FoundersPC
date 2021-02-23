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
    public class CasesRepository : GenericRepositoryAsync<Case>, ICasesRepositoryAsync
    {
        /// <inheritdoc />
        public CasesRepository(FoundersPCHardwareContext context) : base(context) { }

        #region Implementation of ICasesRepositoryAsync

        /// <inheritdoc />
        public override async Task<IEnumerable<Case>> GetAllAsync()
        {
            return await Context.Set<Case>()
                                .Include(@case => @case.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}