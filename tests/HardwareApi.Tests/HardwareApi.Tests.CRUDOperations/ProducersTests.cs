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
using HardwareApi.Tests.MockAbstractions.DataCreation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

#endregion

// todo: check repositories next
// todo: check services
namespace HardwareApi.Tests.CRUDOperations
{
    public class ProducersTests
    {
        private FoundersPCHardwareContext _context;
        private IUnitOfWorkHardwareAPI _unitOfWork;

        [OneTimeSetUp]
        public async Task SetupAsync()
        {
            var options = new DbContextOptionsBuilder<FoundersPCHardwareContext>()
                          .UseInMemoryDatabase("InMem")
                          .Options;

            _context = new FoundersPCHardwareContext(options);

            _unitOfWork = new UnitOfWorkHardwareHardwareAPI(_context,
                                                            new CPUsRepository(_context),
                                                            new ProducersRepository(_context),
                                                            new ProcessorCoresRepository(_context),
                                                            new GPUsRepository(_context),
                                                            new VideoCardCoresRepository(_context),
                                                            new CasesRepository(_context),
                                                            new HDDsRepository(_context),
                                                            new MotherboardsRepository(_context),
                                                            new PowerSuppliersRepository(_context),
                                                            new SSDsRepository(_context),
                                                            new RAMsRepository(_context),
                                                            new NullLogger<UnitOfWorkHardwareHardwareAPI>());

            HC.Count = 1000;

            var producers = HC.CreateProducers()
                              .ToArray();

            var cases = HC.CreateCases(producers);
            var hdds = HC.CreateHDDs(producers);

            foreach (var producer in producers)
                await _unitOfWork.ProducersRepository.AddAsync(producer);

            foreach (var @case in cases)
                await _unitOfWork.CasesRepository.AddAsync(@case);

            foreach (var hdd in hdds)
                await _unitOfWork.HDDsRepository.AddAsync(hdd);

            await _unitOfWork.SaveChangesAsync();
        }

        [Test]
        public async Task PaginationTests([Values(1, 10, 15)] int pageNum)
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