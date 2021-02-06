#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware;
using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware
{
	public class CasesRepository : GenericRepositoryAsync<Case>, ICasesRepositoryAsync
	{
		/// <inheritdoc />
		public CasesRepository(DbContext context) : base(context) { }

		#region Implementation of ICasesRepositoryAsync

		/// <inheritdoc />
		public async Task<Case> GetByIdAsync(int id) =>
			await (await GetAllAsync()).FirstOrDefaultAsync(@case => @case.Id == id);

		/// <inheritdoc />
		public async Task<IQueryable<Case>> GetAllAsync() =>
			await Task.Run(() => _context.Set<Case>().Include(@case => @case.Producer));

		#endregion
	}
}