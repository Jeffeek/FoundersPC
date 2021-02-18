#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.CPU;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.API.Repositories.Hardware.CPU
{
	public class CPUsRepository : GenericRepositoryAsync<Domain.Entities.Hardware.Processor.CPU>, ICPUsRepositoryAsync
	{
		/// <inheritdoc />
		public CPUsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of ICPUsRepositoryAsync

		/// <inheritdoc />
		public async Task<IEnumerable<Domain.Entities.Hardware.Processor.CPU>> GetAllAsync()
		{
			return await _context
						 .Set<Domain.Entities.Hardware.Processor.CPU>()
						 .Include(cpu => cpu.Producer)
						 .Include(cpu => cpu.Core)
						 .ToListAsync();
		}

		#endregion
	}
}