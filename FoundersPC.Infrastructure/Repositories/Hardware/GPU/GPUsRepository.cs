#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.GPU;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware.GPU
{
	public class GPUsRepository : GenericRepositoryAsync<Domain.Entities.Hardware.VideoCard.GPU>, IGPUsRepositoryAsync
	{
		/// <inheritdoc />
		public GPUsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IGPUsRepositoryAsync

		/// <inheritdoc />
		public async Task<Domain.Entities.Hardware.VideoCard.GPU> GetByIdAsync(int id) =>
			await (await GetAllAsync()).FirstOrDefaultAsync(gpu => gpu.Id == id);

		/// <inheritdoc />
		public async Task<IQueryable<Domain.Entities.Hardware.VideoCard.GPU>> GetAllAsync() =>
			await Task.Run(() => _context.Set<Domain.Entities.Hardware.VideoCard.GPU>()
			                             .Include(gpu => gpu.Producer)
			                             .Include(gpu => gpu.Core));

		#endregion
	}
}