#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware
{
	public class PowerSuppliersRepository : GenericRepositoryAsync<PowerSupply>, IPowerSuppliersRepositoryAsync
	{
		/// <inheritdoc />
		public PowerSuppliersRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IPowerSuppliersRepositoryAsync

		public override async Task<PowerSupply> GetByIdAsync(int id)
		{
			var powerSupply = await Context.Set<PowerSupply>().FindAsync(id);

			if (powerSupply is null) return null;

			await Context.Entry(powerSupply).Reference(x => x.Producer).LoadAsync();

			return powerSupply;
		}

		/// <inheritdoc />
		public override async Task<IEnumerable<PowerSupply>> GetAllAsync()
		{
			return await Context.Set<PowerSupply>()
								.Include(powerSupply => powerSupply.Producer)
								.ToListAsync();
		}

		#endregion
	}
}