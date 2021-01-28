#region Using derectives

using System.Threading.Tasks;

#endregion

namespace FoundersPC.Services.Repositories
{
	public interface IUnitOfWork
	{
		ICPURepository ProcessorsRepository { get; }
		IProducersRepository ProducersRepository { get; }
		Task SaveChangesAsync();
	}
}