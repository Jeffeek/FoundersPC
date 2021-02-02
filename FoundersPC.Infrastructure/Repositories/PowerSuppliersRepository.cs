#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public class PowerSuppliersRepository : GenericRepositoryAsync<PowerSupply>, IPowerSuppliersRepositoryAsync
	{
		/// <inheritdoc />
		public PowerSuppliersRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Overrides of GenericRepositoryAsync<Case>

		/// <inheritdoc />
		public override async Task<IQueryable<PowerSupply>> GetAllAsync() =>
			(await base.GetAllAsync()).Include(ps => ps.Producer);

		#endregion
	}
}