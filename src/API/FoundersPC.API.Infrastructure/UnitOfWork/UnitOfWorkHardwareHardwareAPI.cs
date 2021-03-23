#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.CPU;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Infrastructure.UnitOfWork
{
    public class UnitOfWorkHardwareHardwareAPI : IUnitOfWorkHardwareAPI
    {
        private readonly DbContext _context;
        private readonly ILogger<UnitOfWorkHardwareHardwareAPI> _logger;

        public UnitOfWorkHardwareHardwareAPI(FoundersPCHardwareContext context,
                                             ICPUsRepositoryAsync processorsRepository,
                                             IProducersRepositoryAsync producersRepository,
                                             IProcessorCoresRepositoryAsync processorCoresRepository,
                                             IGPUsRepositoryAsync videoCardsRepository,
                                             IVideoCardCoresRepositoryAsync videoCardCoresRepository,
                                             ICasesRepositoryAsync casesRepository,
                                             IHDDsRepositoryAsync hddsRepository,
                                             IMotherboardsRepositoryAsync motherboardsRepository,
                                             IPowerSuppliersRepositoryAsync powerSuppliersRepository,
                                             ISSDsRepositoryAsync ssdsRepository,
                                             IRAMsRepositoryAsync ramsRepository,
                                             ILogger<UnitOfWorkHardwareHardwareAPI> logger)
        {
            _context = context;
            ProcessorsRepository = processorsRepository;
            ProducersRepository = producersRepository;
            ProcessorCoresRepository = processorCoresRepository;
            VideoCardsRepository = videoCardsRepository;
            VideoCardCoresRepository = videoCardCoresRepository;
            CasesRepository = casesRepository;
            HDDsRepository = hddsRepository;
            MotherboardsRepository = motherboardsRepository;
            PowerSuppliersRepository = powerSuppliersRepository;
            SSDsRepository = ssdsRepository;
            RAMsRepository = ramsRepository;
            _logger = logger;
        }

        #region Implementation of IUnitOfWork

        /// <inheritdoc />
        public ICPUsRepositoryAsync ProcessorsRepository { get; }

        /// <inheritdoc />
        public IProducersRepositoryAsync ProducersRepository { get; }

        /// <inheritdoc />
        public IProcessorCoresRepositoryAsync ProcessorCoresRepository { get; }

        /// <inheritdoc />
        public IGPUsRepositoryAsync VideoCardsRepository { get; }

        /// <inheritdoc />
        public IVideoCardCoresRepositoryAsync VideoCardCoresRepository { get; }

        /// <inheritdoc />
        public ICasesRepositoryAsync CasesRepository { get; }

        /// <inheritdoc />
        public IHDDsRepositoryAsync HDDsRepository { get; }

        /// <inheritdoc />
        public IMotherboardsRepositoryAsync MotherboardsRepository { get; }

        /// <inheritdoc />
        public IPowerSuppliersRepositoryAsync PowerSuppliersRepository { get; }

        /// <inheritdoc />
        public ISSDsRepositoryAsync SSDsRepository { get; }

        /// <inheritdoc />
        public IRAMsRepositoryAsync RAMsRepository { get; }

        /// <inheritdoc />
        public async Task<int> SaveChangesAsync()
        {
            _logger.LogInformation("Save changes");
            var saveChangesResult = 0;

            try
            {
                saveChangesResult = await _context.SaveChangesAsync();
                _logger.LogInformation("Successful save changes");
            }
            catch
            {
                saveChangesResult = -1;
                _logger.LogError("Save changes ended with error");
            }

            return saveChangesResult;
        }

        #endregion
    }
}