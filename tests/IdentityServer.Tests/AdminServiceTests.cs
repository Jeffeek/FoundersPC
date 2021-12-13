#region Using namespaces

using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

#endregion

namespace IdentityServer.Tests;

[SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
[SuppressMessage("ReSharper", "ExceptionNotDocumented")]
[TestFixture]
public class AdminServiceTests
{
    //[OneTimeSetUp]
    //public async Task SetUpAsync()
    //{
    //    _context = IdentityDB.GetInMemoryContext();

    //    var mapper = new MapperConfiguration(x => x.AddProfile(typeof(MappingStartup)));

    //    var roles = IdentityServerDataCreation.GenerateRolesWithData();

    //    await _context.Roles.AddRangeAsync(roles);

    //    await _context.SaveChangesAsync();
    //}

    //private ApplicationDbContext _context;

    //[Test]
    //public async Task BlockUserTestAsync()
    //{
    //    var firstUnblockedUser = await _context.Users.FirstOrDefaultAsync(x => !x.IsBlocked
    //                                                                           && x.Role.RoleTitle != ApplicationRoles.Administrator);

    //    await _adminService.BlockUserAsync(firstUnblockedUser.Id);

    //    var actual = firstUnblockedUser.IsBlocked;

    //    var expected = true;

    //    Assert.AreEqual(actual, expected);
    //}

    //[Test]
    //public async Task TryToBlockAdministratorAsync()
    //{
    //    var firstAdmin = await _context.Users.FirstOrDefaultAsync(x => x.Role.RoleTitle == ApplicationRoles.Administrator);

    //    await _adminService.BlockUserAsync(firstAdmin.Id);

    //    var actual = firstAdmin.IsBlocked;

    //    var expected = false;

    //    Assert.AreEqual(actual, expected);
    //}

    //[Test]
    //public async Task BlockTokenTestAsync()
    //{
    //    var tokens = await _context.Tokens
    //                               .Where(x => !x.IsBlocked)
    //                               .ToListAsync();

    //    var randomToken = tokens[new Randomizer().Next(0, tokens.Count)];

    //    await _adminService.BlockAccessTokenAsync(randomToken.Id);

    //    var expected = true;

    //    var actual = randomToken.IsBlocked;

    //    Assert.AreEqual(expected, actual);
    //}

    //[Test]
    //public async Task AreTokensBlockedAfterUserBlockingTestAsync()
    //{
    //    var randomUser = await _context.Users.FirstAsync(x => x.Tokens.Any(t => !t.IsBlocked) && x.Role.RoleTitle != ApplicationRoles.Administrator);

    //    await _adminService.BlockUserAsync(randomUser.Id);

    //    var userTokens = _context.Tokens.Where(x => x.UserId == randomUser.Id);

    //    var actual = userTokens.Where(x => x.ExpirationDate >= DateTime.Now)
    //                           .All(t => t.IsBlocked);

    //    var expected = true;

    //    Assert.AreEqual(actual, expected);
    //}

    //[Test]
    //public async Task AreTokensUnBlockedAfterUserUnBlockingTestAsync()
    //{
    //    var randomUser = await _context.Users.FirstAsync(x =>
    //                                                         x.IsBlocked
    //                                                         && x.Tokens.Any(t => t.ExpirationDate > DateTime.Now)
    //                                                         && x.Role.RoleTitle != ApplicationRoles.Administrator);

    //    await _adminService.UnBlockUserAsync(randomUser.Id);

    //    var userTokens = _context.Tokens.Where(x => x.UserId == randomUser.Id);

    //    var actual = userTokens.Where(x => x.ExpirationDate >= DateTime.Now)
    //                           .All(t => !t.IsBlocked);

    //    var expected = true;

    //    Assert.AreEqual(actual, expected);
    //}
}