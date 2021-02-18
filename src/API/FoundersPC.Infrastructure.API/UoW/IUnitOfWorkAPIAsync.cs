#region Using derectives

using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Application.Interfaces.Repositories.Hardware;
using FoundersPC.Application.Interfaces.Repositories.Hardware.CPU;
using FoundersPC.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.Application.Interfaces.Repositories.Hardware.Memory;

#endregion

namespace FoundersPC.Infrastructure.API.UoW
{
	public interface IUnitOfWorkAPIAsync
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

		Task<int> SaveChangesAsync();
	}
}