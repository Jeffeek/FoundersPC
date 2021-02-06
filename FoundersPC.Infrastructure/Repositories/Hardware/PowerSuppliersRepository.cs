#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware;
using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware
{
	public class PowerSuppliersRepository : GenericRepositoryAsync<PowerSupply>, IPowerSuppliersRepositoryAsync
	{
		/// <inheritdoc />
		public PowerSuppliersRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IPowerSuppliersRepositoryAsync

		/// <inheritdoc />
		public async Task<PowerSupply> GetByIdAsync(int id) =>
			await (await GetAllAsync()).FirstOrDefaultAsync(powerSupply => powerSupply.Id == id);

		/// <inheritdoc />
		public async Task<IQueryable<PowerSupply>> GetAllAsync() =>
			await Task.Run(() => _context.Set<PowerSupply>().Include(powerSupply => powerSupply.Producer));

		#endregion
	}
}