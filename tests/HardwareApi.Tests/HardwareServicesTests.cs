#region Using namespaces

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Persistence;
using HardwareApi.Tests.DataCreation;
using NUnit.Framework;

#endregion

// todo: check repositories next
// todo: check services
namespace HardwareApi.Tests;

[TestFixture]
public class HardwareServicesTests
{
    [OneTimeSetUp]
    public async Task SetupAsync()
    {
        _context = HardwareDb.GetInMemoryContext();

        await _context.BeginTransactionAsync();

        var producers = HardwareApiDataCreation.CreateProducers()
                                               .Take(20)
                                               .ToArray();

        var cases = HardwareApiDataCreation.CreateCases()
                                           .Take(50);

        await _context.Set<Producer>()
                      .AddRangeAsync(producers);

        await _context.Set<Case>()
                      .AddRangeAsync(cases);

        await _context.CommitTransactionAsync();
    }

    private ApplicationDbContext _context;

    //[Test]
    //public async Task PaginationTestAsync([Values(1, 10, 15)] int pageNum)
    //{
    //    var prods = (await _unitOfWork.ProducersRepository.GetPaginateableAsync(pageNum)).ToArray();

    //    Assert.AreEqual(prods.Last()
    //                         .Id,
    //                    pageNum * FoundersPCConstants.PageSize);

    //    Assert.AreEqual(prods.First()
    //                         .Id,
    //                    pageNum * FoundersPCConstants.PageSize - (FoundersPCConstants.PageSize - 1));
    //}
}