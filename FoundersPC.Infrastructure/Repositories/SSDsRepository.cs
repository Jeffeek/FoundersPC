#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware.Memory;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public class SSDsRepository : GenericRepositoryAsync<SSD>, ISSDsRepositoryAsync
	{
		/// <inheritdoc />
		public SSDsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Overrides of GenericRepositoryAsync<Case>

		/// <inheritdoc />
		public override async Task<IQueryable<SSD>> GetAllAsync() =>
			(await base.GetAllAsync()).Include(ssd => ssd.Producer);

		#endregion
	}
}