#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.Domain.Entities.Hardware.Memory;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware.Memory
{
	public class SSDsRepository : GenericRepositoryAsync<SSD>, ISSDsRepositoryAsync
	{
		/// <inheritdoc />
		public SSDsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of ISSDsRepositoryAsync

		/// <inheritdoc />
		public async Task<SSD> GetByIdAsync(int id) =>
			await (await GetAllAsync()).FirstOrDefaultAsync(ssd => ssd.Id == id);

		/// <inheritdoc />
		public async Task<IQueryable<SSD>> GetAllAsync() =>
			await Task.Run(() => _context.Set<SSD>().Include(ssd => ssd.Producer));

		#endregion
	}
}