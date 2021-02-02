#region Using derectives

using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;

#endregion

namespace FoundersPC.Infrastructure.UoW
{
	public interface IUnitOfWorkAsync
	{
		IProducersRepositoryAsync ProducersRepository { get; }

		ICPUsRepositoryAsync ProcessorsRepository { get; }
		IProcessorCoresRepositoryAsync ProcessorCoresRepository { get; }

		IGPUsRepositoryAsync VideoCardsRepository { get; }
		IVideoCardCoresRepositoryAsync VideoCardCoresRepository { get; }

		ICasesRepositoryAsync CasesRepository { get; }
		IHDDsRepositoryAsync HDDsRepository { get; }
		IMotherboardsRepositoryAsync MotherboardsRepository { get; }
		IPowerSuppliersRepositoryAsync PowerSuppliersRepository { get; }
		ISSDsRepositoryAsync SSDsRepository { get; }
		IRAMsRepositoryAsync RAMsRepository { get; }

		Task<bool> SaveChangesAsync();
	}
}