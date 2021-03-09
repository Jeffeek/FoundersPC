#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.GPU
{
	public class GPUsRepository : GenericRepositoryAsync<Domain.Entities.Hardware.VideoCard.GPU>, IGPUsRepositoryAsync
	{
		/// <inheritdoc />
		public GPUsRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IGPUsRepositoryAsync

		public override async Task<Domain.Entities.Hardware.VideoCard.GPU> GetByIdAsync(int id)
		{
			var gpu = await Context.Set<Domain.Entities.Hardware.VideoCard.GPU>().FindAsync(id);

			if (gpu is null) return null;

			await Context.Entry(gpu).Reference(x => x.Producer).LoadAsync();
			await Context.Entry(gpu).Reference(x => x.Core).LoadAsync();

			return gpu;
		}

		/// <inheritdoc />
		public override async Task<IEnumerable<Domain.Entities.Hardware.VideoCard.GPU>> GetAllAsync()
		{
			return await Context
						 .Set<Domain.Entities.Hardware.VideoCard.GPU>()
						 .Include(gpu => gpu.Producer)
						 .Include(gpu => gpu.Core)
						 .ToListAsync();
		}

		#endregion
	}
}