#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.GPU
{
	public class GPUsRepository : RepositoryBase<Models.Hardware.VideoCard.GPU>, IGPUsRepository
	{
		/// <inheritdoc />
		public GPUsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IGPUsRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.VideoCard.GPU>> GetAllGPUsAsync() => await GetAll()
			                                                                   .Include(gpu => gpu.Producer)
			                                                                   .Include(x => x.Core)
			                                                                   .ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.VideoCard.GPU> GetGPUByIdAsync(int gpuId) =>
			await FindBy(gpu => gpu.Id == gpuId)
			      .Include(gpu => gpu.Producer)
			      .Include(gpu => gpu.Core)
			      .FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateGPU(Models.Hardware.VideoCard.GPU gpu) => await Task.Run(() => Create(gpu));
		
		/// <inheritdoc />
		public async Task UpdateGPU(Models.Hardware.VideoCard.GPU gpu) => await Task.Run(() => Update(gpu));
		
		/// <inheritdoc />
		public async Task DeleteGPU(Models.Hardware.VideoCard.GPU gpu) => await Task.Run(() => Delete(gpu));
		

		#endregion
	}
}