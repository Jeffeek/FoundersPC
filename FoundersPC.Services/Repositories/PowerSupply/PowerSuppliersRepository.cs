#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.PowerSupply
{
	public class PowerSuppliersRepository : RepositoryBase<Models.Hardware.PowerSupply>, IPowerSuppliersRepository
	{
		/// <inheritdoc />
		public PowerSuppliersRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IPowerSuppliersRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.PowerSupply>> GetAllPowerSuppliersAsync() => await GetAll()
			.Include(powerSupply => powerSupply.Producer)
			.ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.PowerSupply> GetPowerSupplyByIdAsync(int powerSupplyId) =>
			await FindBy(powerSupply => powerSupply.Id == powerSupplyId)
			      .Include(powerSupply => powerSupply.Producer)
			      .FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreatePowerSupply(Models.Hardware.PowerSupply powerSupply) => await Task.Run(() => Create(powerSupply));
		
		/// <inheritdoc />
		public async Task UpdatePowerSupply(Models.Hardware.PowerSupply powerSupply) => await Task.Run(() => Update(powerSupply));
		
		/// <inheritdoc />
		public async Task DeletePowerSupply(Models.Hardware.PowerSupply powerSupply) => await Task.Run(() => Delete(powerSupply));
		

		#endregion
	}
}