#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.Domain.Entities.Hardware.Memory;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware.Memory
{
	public class RAMsRepository : GenericRepositoryAsync<RAM>, IRAMsRepositoryAsync
	{
		/// <inheritdoc />
		public RAMsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IRAMsRepositoryAsync

		/// <inheritdoc />
		public async Task<RAM> GetByIdAsync(int id) =>
			await (await GetAllAsync()).FirstOrDefaultAsync(ram => ram.Id == id);

		/// <inheritdoc />
		public async Task<IQueryable<RAM>> GetAllAsync() =>
			await Task.Run(() => _context.Set<RAM>().Include(ram => ram.Producer));

		#endregion
	}
}