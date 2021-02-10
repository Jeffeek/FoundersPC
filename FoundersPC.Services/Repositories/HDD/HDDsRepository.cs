#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.HDD
{
	public class HDDsRepository : RepositoryBase<Models.Hardware.Memory.HDD>, IHDDsRepository
	{
		/// <inheritdoc />
		public HDDsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IHDDsRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.Memory.HDD>> GetAllHDDsAsync() => await GetAll()
			.Include(x => x.Producer)
			.ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.Memory.HDD> GetHDDByIdAsync(int hddId) =>
			await FindBy(hdd => hdd.Id == hddId)
			      .Include(hdd => hdd.Producer)
			      .FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateHDD(Models.Hardware.Memory.HDD hdd) => await Task.Run(() => Create(hdd));
		
		/// <inheritdoc />
		public async Task UpdateHDD(Models.Hardware.Memory.HDD hdd) => await Task.Run(() => Update(hdd));
		
		/// <inheritdoc />
		public async Task DeleteHDD(Models.Hardware.Memory.HDD hdd) => await Task.Run(() => Delete(hdd));
		

		#endregion
	}
}