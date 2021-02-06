#region Using derectives

using System.Linq;
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
		public async Task<Motherboard> GetByIdAsync(int id) =>
			await (await GetAllAsync()).FirstOrDefaultAsync(motherboard => motherboard.Id == id);

		/// <inheritdoc />
		public async Task<IQueryable<Motherboard>> GetAllAsync() =>
			await Task.Run(() => _context.Set<Motherboard>().Include(motherboard => motherboard.Producer));

		#endregion
	}
}