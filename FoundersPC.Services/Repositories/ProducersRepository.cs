#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories
{
	public class ProducersRepository : RepositoryBase<Producer>, IProducersRepository
	{
		/// <inheritdoc />
		public ProducersRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IProducersRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Producer>> GetAllProducersAsync() => await GetAll().ToListAsync();

		/// <inheritdoc />
		public async Task<Producer> GetProducerByIdAsync(int producerId) =>
			await FindBy(producer => producer.Id == producerId).FirstOrDefaultAsync();

		/// <inheritdoc />
		public void CreateProducer(Producer producer)
		{
			Create(producer);
		}

		/// <inheritdoc />
		public void UpdateProducer(Producer producer)
		{
			Update(producer);
		}

		/// <inheritdoc />
		public void DeleteProducer(Producer producer)
		{
			Delete(producer);
		}

		#endregion
	}
}