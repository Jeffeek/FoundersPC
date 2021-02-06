#region Using derectives

using System.Linq;
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
		public async Task<HDD> GetByIdAsync(int id) =>
			await (await GetAllAsync()).FirstOrDefaultAsync(hdd => hdd.Id == id);

		/// <inheritdoc />
		public async Task<IQueryable<HDD>> GetAllAsync() =>
			await Task.Run(() => _context.Set<HDD>().Include(hdd => hdd.Producer));

		#endregion
	}
}