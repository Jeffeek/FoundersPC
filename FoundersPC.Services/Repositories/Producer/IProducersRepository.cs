#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.Producer
{
	public interface IProducersRepository : IRepository<Models.Hardware.Producer>
	{
		Task<IEnumerable<Models.Hardware.Producer>> GetAllProducersAsync();
		Task<Models.Hardware.Producer> GetProducerByIdAsync(int producerId);
		Task CreateProducer(Models.Hardware.Producer producer);
		Task UpdateProducer(Models.Hardware.Producer producer);
		Task DeleteProducer(Models.Hardware.Producer producer);
	}
}