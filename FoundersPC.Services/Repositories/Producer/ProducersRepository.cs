#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.Producer
{
	public class ProducersRepository : RepositoryBase<Models.Hardware.Producer>, IProducersRepository
	{
		/// <inheritdoc />
		public ProducersRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IProducersRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.Producer>> GetAllProducersAsync() => await GetAll().ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.Producer> GetProducerByIdAsync(int producerId) =>
			await FindBy(producer => producer.Id == producerId).FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateProducer(Models.Hardware.Producer producer) => await Task.Run(() => Create(producer));
		
		/// <inheritdoc />
		public async Task UpdateProducer(Models.Hardware.Producer producer) => await Task.Run(() => Update(producer));

		/// <inheritdoc />
		public async Task DeleteProducer(Models.Hardware.Producer producer) => await Task.Run(() => Delete(producer));

		#endregion
	}
}