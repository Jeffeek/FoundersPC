#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware.VideoCard;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public class GPUsRepository : GenericRepositoryAsync<GPU>, IGPUsRepositoryAsync
	{
		/// <inheritdoc />
		public GPUsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Overrides of GenericRepositoryAsync<Case>

		/// <inheritdoc />
		public override async Task<IQueryable<GPU>> GetAllAsync() => (await base.GetAllAsync())
		                                                             .Include(gpu => gpu.Producer)
		                                                             .Include(gpu => gpu.Core);

		#endregion
	}
}