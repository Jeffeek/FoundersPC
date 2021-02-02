#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public class MotherboardsRepository : GenericRepositoryAsync<Motherboard>, IMotherboardsRepositoryAsync
	{
		/// <inheritdoc />
		public MotherboardsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Overrides of GenericRepositoryAsync<Case>

		/// <inheritdoc />
		public override async Task<IQueryable<Motherboard>> GetAllAsync() =>
			(await base.GetAllAsync()).Include(motherboard => motherboard.Producer);

		#endregion
	}
}