#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.RAM
{
	public class RAMsRepository : RepositoryBase<Models.Hardware.Memory.RAM>, IRAMsRepository
	{
		/// <inheritdoc />
		public RAMsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IRAMsRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.Memory.RAM>> GetAllRAMsAsync() => await GetAll()
			.Include(x => x.Producer)
			.ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.Memory.RAM> GetRAMByIdAsync(int cpuId) =>
			await FindBy(cpu => cpu.Id == cpuId)
			      .Include(x => x.Producer)
			      .FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateRAM(Models.Hardware.Memory.RAM ram) => await Task.Run(() => Create(ram));
		
		/// <inheritdoc />
		public async Task UpdateRAM(Models.Hardware.Memory.RAM ram) => await Task.Run(() => Update(ram));
		
		/// <inheritdoc />
		public async Task DeleteRAM(Models.Hardware.Memory.RAM ram) => await Task.Run(() => Delete(ram));
		

		#endregion
	}
}