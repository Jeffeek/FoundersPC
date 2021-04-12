#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.Processor;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.VideoCard;

#endregion

namespace FoundersPC.API.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkHardwareAPI
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