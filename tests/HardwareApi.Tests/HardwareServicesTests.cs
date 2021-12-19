#region Using namespaces

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Domain.Entities.Identity.Users;
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
    private async Task InitializeUsers()
    {
        var systemRole = HardwareApiDataCreation.CreateSystemRole();
        var systemUser = HardwareApiDataCreation.CreateSystemUser(systemRole);

        await _context.Set<ApplicationRole>()
                      .AddAsync(systemRole);

        await _context.Set<ApplicationUser>()
                      .AddAsync(systemUser);
    }

    private async Task TaskInitializeHardware()
    {
        var hardwareTypes = HardwareApiDataCreation.CreateHardwareTypes();

        await _context.Set<HardwareType>()
                      .AddRangeAsync(hardwareTypes);

        var cases = HardwareApiDataCreation.CreateCases()
                                           .Take(10);

        await _context.Set<Case>()
                      .AddRangeAsync(cases);
    }

    [OneTimeSetUp]
    public async Task SetupAsync()
    {
        _context = HardwareDb.GetInMemoryContext();
        await InitializeUsers();
        await TaskInitializeHardware();

        await _context.SaveChangesAsync();
    }

    private ApplicationDbContext _context;

    [Test]
    public void A() { }
}