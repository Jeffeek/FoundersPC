#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories;
using FoundersPC.API.Application.Interfaces.Repositories.Memory;
using FoundersPC.API.Application.Interfaces.Repositories.Processor;
using FoundersPC.API.Application.Interfaces.Repositories.VideoCard;

#endregion

namespace FoundersPC.API.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkHardwareAPI
    {
        IProducersRepositoryAsync ProducersRepository { get; }

        IProcessorsRepositoryAsync ProcessorsRepository { get; }

        IProcessorCoresRepositoryAsync ProcessorCoresRepository { get; }

        IVideoCardsRepositoryAsync VideoCardsRepository { get; }

        IVideoCardCoresRepositoryAsync VideoCardCoresRepository { get; }

        ICasesRepositoryAsync CasesRepository { get; }

        IHardDrivesRepositoryAsync HardDrivesRepository { get; }

        IMotherboardsRepositoryAsync MotherboardsRepository { get; }

        IPowerSuppliersRepositoryAsync PowerSuppliersRepository { get; }

        ISolidStateDrivesRepositoryAsync SolidStateDrivesRepository { get; }

        IRandomAccessMemoryRepositoryAsync RandomAccessMemoryRepository { get; }

        Task<int> SaveChangesAsync();
    }
}