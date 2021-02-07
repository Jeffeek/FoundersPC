#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.CPU;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware.CPU
{
	public class CPUsRepository : GenericRepositoryAsync<Domain.Entities.Hardware.Processor.CPU>, ICPUsRepositoryAsync
	{
		/// <inheritdoc />
		public CPUsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of ICPUsRepositoryAsync

		/// <inheritdoc />
		public async Task<Domain.Entities.Hardware.Processor.CPU> GetByIdAsync(int id) =>
			await (await GetAllAsync()).FirstOrDefaultAsync(cpu => cpu.Id == id);

		/// <inheritdoc />
		public async Task<IQueryable<Domain.Entities.Hardware.Processor.CPU>> GetAllAsync() =>
			await Task.Run(() => _context.Set<Domain.Entities.Hardware.Processor.CPU>()
			                             .Include(cpu => cpu.Producer)
			                             .Include(cpu => cpu.Core)
			                             .AsNoTracking());

		#endregion
	}
}