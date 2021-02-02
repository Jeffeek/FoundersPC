#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware.Processor;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public class CPUsRepository : GenericRepositoryAsync<CPU>, ICPUsRepositoryAsync
	{
		/// <inheritdoc />
		public CPUsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Overrides of GenericRepositoryAsync<Case>

		/// <inheritdoc />
		public override async Task<IQueryable<CPU>> GetAllAsync() =>
			(await base.GetAllAsync()).Include(cpu => cpu.Producer)
			                          .Include(cpu => cpu.Core);

		#endregion
	}
}