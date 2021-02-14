#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware;
using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware
{
	public class MotherboardsRepository : GenericRepositoryAsync<Motherboard>, IMotherboardsRepositoryAsync
	{
		/// <inheritdoc />
		public MotherboardsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IMotherboardsRepositoryAsync

		/// <inheritdoc />
		public async Task<IEnumerable<Motherboard>> GetAllAsync() => await _context.Set<Motherboard>()
			                                                             .Include(motherboard => motherboard.Producer)
			                                                             .ToListAsync();

		#endregion
	}
}