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