#region Using derectives

using System.Threading.Tasks;
using FoundersPC.Services.Repositories.CPU;
using FoundersPC.Services.Repositories.ProcessorLineup;
using FoundersPC.Services.Repositories.Producer;

#endregion

namespace FoundersPC.Services.Repositories.UoW
{
	public interface IUnitOfWork
	{
		IProcessorLineupsRepository ProcessorLineupsRepository { get; }
		ICPUsRepository ProcessorsRepository { get; }
		IProducersRepository ProducersRepository { get; }
		Task<bool> SaveChangesAsync();
	}
}