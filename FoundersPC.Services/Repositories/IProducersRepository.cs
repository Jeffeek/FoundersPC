#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Models;

#endregion

namespace FoundersPC.Services.Repositories
{
	public interface IProducersRepository : IRepository<Producer>
	{
		Task<IEnumerable<Producer>> GetAllProducersAsync();
		Task<Producer> GetProducerByIdAsync(int producerId);
		void CreateProducer(Producer producer);
		void UpdateProducer(Producer producer);
		void DeleteProducer(Producer producer);
	}
}