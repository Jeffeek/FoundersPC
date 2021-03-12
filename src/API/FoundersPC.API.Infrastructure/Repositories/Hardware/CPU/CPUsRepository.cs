#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.CPU;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.CPU
{
	public class CPUsRepository : GenericRepositoryAsync<Domain.Entities.Hardware.Processor.CPU>, ICPUsRepositoryAsync
	{
		/// <inheritdoc />
		public CPUsRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of ICPUsRepositoryAsync

		public override async Task<Domain.Entities.Hardware.Processor.CPU> GetByIdAsync(int id)
		{
			var cpu = await Context.Set<Domain.Entities.Hardware.Processor.CPU>().FindAsync(id);

			if (cpu is null) return null;

			await Context.Entry(cpu).Reference(x => x.Producer).LoadAsync();
			await Context.Entry(cpu).Reference(x => x.Core).LoadAsync();

			return cpu;
		}

		/// <inheritdoc />
		public override async Task<IEnumerable<Domain.Entities.Hardware.Processor.CPU>> GetAllAsync()
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