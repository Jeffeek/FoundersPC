#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.SSD
{
	public class SSDsRepository : RepositoryBase<Models.Hardware.Memory.SSD>, ISSDsRepository
	{
		/// <inheritdoc />
		public SSDsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of ISSDsRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.Memory.SSD>> GetAllSSDsAsync() => await GetAll()
			.Include(x => x.Producer)
			.ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.Memory.SSD> GetSSDByIdAsync(int cpuId) =>
			await FindBy(cpu => cpu.Id == cpuId)
			      .Include(x => x.Producer)
			      .FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateSSD(Models.Hardware.Memory.SSD ssd) => await Task.Run(() => Create(ssd));
		
		/// <inheritdoc />
		public async Task UpdateSSD(Models.Hardware.Memory.SSD ssd) => await Task.Run(() => Update(ssd));
		
		/// <inheritdoc />
		public async Task DeleteSSD(Models.Hardware.Memory.SSD ssd) => await Task.Run(() => Delete(ssd));
		

		#endregion
	}
}