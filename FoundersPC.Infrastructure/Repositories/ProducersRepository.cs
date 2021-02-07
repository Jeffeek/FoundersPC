#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public class ProducersRepository : GenericRepositoryAsync<Producer>, IProducersRepositoryAsync
	{
		/// <inheritdoc />
		public ProducersRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IProducersRepositoryAsync

		/// <inheritdoc />
		public async Task<Producer> GetByIdAsync(int id) =>
			await (await GetAllAsync()).FirstOrDefaultAsync(powerSupply => powerSupply.Id == id);

		/// <inheritdoc />
		public async Task<IQueryable<Producer>> GetAllAsync(bool includeEquipment = true)
		{
			var producers = await Task.Run(() => _context.Set<Producer>().AsQueryable());
			if (!includeEquipment) return producers;
			producers.Include(producer => producer.Motherboards)
			         .ThenInclude(motherboard => motherboard.Producer)
			         .Include(producer => producer.PowerSupplies)
			         .ThenInclude(powerSupply => powerSupply.Producer)
			         .Include(producer => producer.Cases)
			         .ThenInclude(@case => @case.Producer)
			         .Include(producer => producer.HardDrives)
			         .ThenInclude(hdd => hdd.Producer)
			         .Include(producer => producer.SolidStateDrive)
			         .ThenInclude(ssd => ssd.Producer)
			         .Include(producer => producer.Processors)
			         .ThenInclude(cpu => cpu.Producer)
			         .Include(producer => producer.VideoCards)
			         .ThenInclude(videoCardCore => videoCardCore.Producer)
			         .Include(producer => producer.RandomAccessMemory);

			return producers;
		}

		/// <inheritdoc />
		public async Task<bool> AnyAsync(Producer producer)
		{
			await _context.Set<Producer>().LoadAsync();
			return await _context.Set<Producer>().AnyAsync(prod => prod.Equals(producer));
		}

		#endregion
	}
}