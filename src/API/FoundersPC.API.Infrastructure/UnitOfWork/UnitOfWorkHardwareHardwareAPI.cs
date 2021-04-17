#region Using namespaces

using System;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories;
using FoundersPC.API.Application.Interfaces.Repositories.Memory;
using FoundersPC.API.Application.Interfaces.Repositories.Processor;
using FoundersPC.API.Application.Interfaces.Repositories.VideoCard;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Infrastructure.UnitOfWork
{
    public class UnitOfWorkHardwareHardwareAPI : IUnitOfWorkHardwareAPI
    {
        private readonly DbContext _context;
        private readonly ILogger<UnitOfWorkHardwareHardwareAPI> _logger;

        public UnitOfWorkHardwareHardwareAPI(DbContext context,
                                             IProcessorsRepositoryAsync processorsRepository,
                                             IProducersRepositoryAsync producersRepository,
                                             IProcessorCoresRepositoryAsync processorCoresRepository,
                                             IVideoCardsRepositoryAsync videoCardsRepository,
                                             IVideoCardCoresRepositoryAsync videoCardCoresRepository,
                                             ICasesRepositoryAsync casesRepository,
                                             IHardDrivesRepositoryAsync hardDriveDisksRepository,
                                             IMotherboardsRepositoryAsync motherboardsRepository,
                                             IPowerSuppliersRepositoryAsync powerSuppliersRepository,
                                             ISolidStateDrivesRepositoryAsync solidStateDrivesRepository,
                                             IRandomAccessMemoryRepositoryAsync ramsRepository,
                                             ILogger<UnitOfWorkHardwareHardwareAPI> logger)
        {
            _context = context;
            ProcessorsRepository = processorsRepository;
            ProducersRepository = producersRepository;
            ProcessorCoresRepository = processorCoresRepository;
            VideoCardsRepository = videoCardsRepository;
            VideoCardCoresRepository = videoCardCoresRepository;
            CasesRepository = casesRepository;
            HardDrivesRepository = hardDriveDisksRepository;
            MotherboardsRepository = motherboardsRepository;
            PowerSuppliersRepository = powerSuppliersRepository;
            SolidStateDrivesRepository = solidStateDrivesRepository;
            RandomAccessMemoryRepository = ramsRepository;
            _logger = logger;
        }

        #region Implementation of IUnitOfWork

        /// <inheritdoc/>
        public IProcessorsRepositoryAsync ProcessorsRepository { get; }

        /// <inheritdoc/>
        public IProducersRepositoryAsync ProducersRepository { get; }

        /// <inheritdoc/>
        public IProcessorCoresRepositoryAsync ProcessorCoresRepository { get; }

        /// <inheritdoc/>
        public IVideoCardsRepositoryAsync VideoCardsRepository { get; }

        /// <inheritdoc/>
        public IVideoCardCoresRepositoryAsync VideoCardCoresRepository { get; }

        /// <inheritdoc/>
        public ICasesRepositoryAsync CasesRepository { get; }

        /// <inheritdoc/>
        public IHardDrivesRepositoryAsync HardDrivesRepository { get; }

        /// <inheritdoc/>
        public IMotherboardsRepositoryAsync MotherboardsRepository { get; }

        /// <inheritdoc/>
        public IPowerSuppliersRepositoryAsync PowerSuppliersRepository { get; }

        /// <inheritdoc/>
        public ISolidStateDrivesRepositoryAsync SolidStateDrivesRepository { get; }

        /// <inheritdoc/>
        public IRandomAccessMemoryRepositoryAsync RandomAccessMemoryRepository { get; }

        /// <inheritdoc/>
        public async Task<int> SaveChangesAsync()
        {
            _logger.LogInformation("Save changes");
            int saveChangesResult;

            try
            {
                saveChangesResult = await _context.SaveChangesAsync();
                _logger.LogInformation($"Successful save changes in {nameof(UnitOfWorkHardwareHardwareAPI)}");
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                                 $"Error happened when tried to make save changes in {nameof(UnitOfWorkHardwareHardwareAPI)}");

                saveChangesResult = -1;
            }

            return saveChangesResult;
        }

        #endregion
    }
}