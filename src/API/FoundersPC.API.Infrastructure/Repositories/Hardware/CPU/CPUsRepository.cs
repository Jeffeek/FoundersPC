#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.CPU;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.ApplicationShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.CPU
{
    public class CPUsRepository : GenericRepositoryAsync<Domain.Entities.Hardware.Processor.CPU>, ICPUsRepositoryAsync
    {
        /// <inheritdoc />
        public CPUsRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of ICPUsRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<Domain.Entities.Hardware.Processor.CPU>> GetAllAsync()
        {
            return await Context
                         .Set<Domain.Entities.Hardware.Processor.CPU>()
                         .Include(cpu => cpu.Producer)
                         .Include(cpu => cpu.Core)
                         .ToListAsync();
        }

        #endregion
    }
}