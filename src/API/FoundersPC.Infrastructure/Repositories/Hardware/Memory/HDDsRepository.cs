#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.Domain.Entities.Hardware.Memory;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware.Memory
{
	public class HDDsRepository : GenericRepositoryAsync<HDD>, IHDDsRepositoryAsync
	{
		/// <inheritdoc />
		public HDDsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IHDDsRepositoryAsync

		/// <inheritdoc />
		public async Task<IEnumerable<HDD>> GetAllAsync()
		{
			return await _context.Set<HDD>()
								 .Include(hdd => hdd.Producer)
								 .ToListAsync();
		}

		#endregion
	}
}