#region Using namespaces

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.API.Infrastructure.Repositories;
using FoundersPC.API.Infrastructure.Repositories.Hardware;
using FoundersPC.API.Infrastructure.Repositories.Hardware.Memory;
using FoundersPC.API.Infrastructure.Repositories.Hardware.Processor;
using FoundersPC.API.Infrastructure.Repositories.Hardware.VideoCard;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using HardwareApi.Tests.DataCreation;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

#endregion

// todo: check repositories next
// todo: check services
namespace HardwareApi.Tests
{
    [TestFixture]
    public class HardwareServicesTests
    {
        [OneTimeSetUp]
        public async Task SetupAsync()
        {
            _context = HardwareDB.GetInMemoryContext();

            _unitOfWork = new UnitOfWorkHardwareHardwareAPI(_context,
                                                            new ProcessorsRepository(_context),
                                                            new ProducersRepository(_context),
                                                            new ProcessorCoresRepository(_context),
                                                            new VideoCardsRepository(_context),
                                                            new VideoCardCoresRepository(_context),
                                                            new CasesRepository(_context),
                                                            new HardDrivesRepository(_context),
                                                            new MotherboardsRepository(_context),
                                                            new PowerSuppliersRepository(_context),
                                                            new SolidStateDrivesRepository(_context),
                                                            new RandomAccessMemoryRepository(_context),
                                                            new NullLogger<UnitOfWorkHardwareHardwareAPI>());

            var producers = HardwareApiDataCreation.CreateProducers()
                                                   .Take(1000)
                                                   .ToArray();

            var cases = HardwareApiDataCreation.CreateCases()
                                               .Take(1000);

            foreach (var producer in producers)
                await _unitOfWork.ProducersRepository.AddAsync(producer);

            await _unitOfWork.SaveChangesAsync()
                             .ConfigureAwait(false);
        }

        private FoundersPCHardwareContext _context;
        private IUnitOfWorkHardwareAPI _unitOfWork;

        [Test]
        public async Task PaginationTestAsync([Values(1, 10, 15)] int pageNum)
        {
            var prods = (await _unitOfWork.ProducersRepository.GetPaginateableAsync(pageNum)).ToArray();

            Assert.AreEqual(prods.Last()
                                 .Id,
                            pageNum * FoundersPCConstants.PageSize);

            Assert.AreEqual(prods.First()
                                 .Id,
                            pageNum * FoundersPCConstants.PageSize - (FoundersPCConstants.PageSize - 1));
        }
    }
}