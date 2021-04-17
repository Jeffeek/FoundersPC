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
    public class CasesRepository : GenericRepositoryAsync<CaseEntity>,
                                   ICasesRepositoryAsync
    {
        /// <inheritdoc/>
        public CasesRepository(DbContext context) : base(context) { }

        #region Implementation of IPaginateableRepository<CaseEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<CaseEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.ProducerEntity)
                  .ToListAsync();

        #endregion

        #region Implementation of ICasesRepositoryAsync

        public override async Task<CaseEntity> GetByIdAsync(int id)
        {
            var @case = await Context.Set<CaseEntity>()
                                     .FindAsync(id);

            if (@case is null)
                return null;

            await Context.Entry(@case)
                         .Reference(x => x.ProducerEntity)
                         .LoadAsync();

            return @case;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<CaseEntity>> GetAllAsync()
        {
            return await Context.Set<CaseEntity>()
                                .Include(@case => @case.ProducerEntity)
                                .ToListAsync();
        }

        #endregion
    }
}