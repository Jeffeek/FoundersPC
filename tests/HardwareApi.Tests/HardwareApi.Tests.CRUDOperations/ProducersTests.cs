#region Using namespaces

using System.Linq;
using System.Threading.Tasks;
using HardwareApi.Tests.MockAbstractions.Contexts;
using HardwareApi.Tests.MockAbstractions.DataCreation;
using NUnit.Framework;

#endregion

// todo: check repositories next
// todo: check services
namespace HardwareApi.Tests.CRUDOperations
{
    public class ProducersTests
    {
        private MockHardwareDbContext _context;

        [SetUp]
        public async Task Setup()
        {
            _context = new MockHardwareDbContext();

            var producers = HardwareCreation.CreateProducers()
                                            .ToList();

            await _context.Context.Producers.AddRangeAsync(producers);

            await _context.Context.SaveChangesAsync();
        }
    }
}