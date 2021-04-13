#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories;
using FoundersPC.API.Domain.Entities;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware
{
    public class CasesRepository : GenericRepositoryAsync<Case>,
                                   ICasesRepositoryAsync
    {
        /// <inheritdoc/>
        public CasesRepository(DbContext context) : base(context) { }

        #region Implementation of IPaginateableRepository<Case>

        /// <inheritdoc/>
        public async Task<IEnumerable<Case>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .ToListAsync();

        #endregion

        #region Implementation of ICasesRepositoryAsync

        public override async Task<Case> GetByIdAsync(int id)
        {
            var @case = await Context.Set<Case>()
                                     .FindAsync(id);

            if (@case is null)
                return null;

            await Context.Entry(@case)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            return @case;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Case>> GetAllAsync()
        {
            return await Context.Set<Case>()
                                .Include(@case => @case.Producer)
                                .ToListAsync();
        }

        #endregion
    }
}