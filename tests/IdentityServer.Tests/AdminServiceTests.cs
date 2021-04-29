#region Using namespaces

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Application.Mappings;
using FoundersPC.Identity.Infrastructure.Contexts;
using FoundersPC.Identity.Infrastructure.Repositories.Logs;
using FoundersPC.Identity.Infrastructure.Repositories.Tokens;
using FoundersPC.Identity.Infrastructure.Repositories.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Administration.Admin_Services;
using FoundersPC.Identity.Services.Encryption_Services;
using FoundersPC.Identity.Services.Token_Services;
using IdentityServer.Tests.DataCreation;
using IdentityServer.Tests.MockServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using NUnit.Framework.Internal;

#endregion

namespace IdentityServer.Tests
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [TestFixture]
    public class AdminServiceTests
    {
        [OneTimeSetUp]
        public async Task SetUpAsync()
        {
            _context = IdentityDB.GetInMemoryContext();

            var mapper = new MapperConfiguration(x => x.AddProfile(typeof(MappingStartup)));

            var unitOfWork = new UnitOfWorkUsersIdentity(new UsersRepository(_context),
                                                         new RolesRepository(_context),
                                                         new AccessTokensLogsRepository(_context),
                                                         new UsersEntrancesLogsRepository(_context),
                                                         new AccessTokensRepository(_context),
                                                         _context,
                                                         new NullLogger<UnitOfWorkUsersIdentity>());

            _adminService = new AdminService(new MockEmailService(),
                                             unitOfWork,
                                             new AccessUsersTokensService(unitOfWork,
                                                                          new AccessTokenReservationService(unitOfWork,
                                                                                                            new TokenEncryptorService(),
                                                                                                            new MockEmailService(),
                                                                                                            new NullLogger<AccessTokenReservationService>()),
                                                                          new AccessTokensBlockingService(new NullLogger<AccessTokensBlockingService>(),
                                                                                                          unitOfWork),
                                                                          new AccessTokensRequestsService(new NullLogger<AccessTokensRequestsService>(),
                                                                                                          unitOfWork),
                                                                          new AccessTokensStatusService(unitOfWork,
                                                                                                        new NullLogger<AccessTokensStatusService>()),
                                                                          new Mapper(mapper),
                                                                          new NullLogger<AccessUsersTokensService>()),
                                             new NullLogger<AdminService>());

            var roles = IdentityServerDataCreation.GenerateRolesWithData();

            await _context.Roles.AddRangeAsync(roles);

            await _context.SaveChangesAsync();
        }

        private FoundersPCUsersContext _context;

        private IAdminService _adminService;

        [Test]
        public async Task BlockUserTestAsync()
        {
            var firstUnblockedUser = await _context.Users.FirstOrDefaultAsync(x => !x.IsBlocked
                                                                                   && x.Role.RoleTitle != ApplicationRoles.Administrator);

            await _adminService.BlockUserAsync(firstUnblockedUser.Id);

            var actual = firstUnblockedUser.IsBlocked;

            var expected = true;

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public async Task TryToBlockAdministratorAsync()
        {
            var firstAdmin = await _context.Users.FirstOrDefaultAsync(x => x.Role.RoleTitle == ApplicationRoles.Administrator);

            await _adminService.BlockUserAsync(firstAdmin.Id);

            var actual = firstAdmin.IsBlocked;

            var expected = false;

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public async Task BlockTokenTestAsync()
        {
            var tokens = await _context.UsersTokens
                                       .Where(x => !x.IsBlocked)
                                       .ToListAsync();

            var randomToken = tokens[new Randomizer().Next(0, tokens.Count)];

            await _adminService.BlockAccessTokenAsync(randomToken.Id);

            var expected = true;

            var actual = randomToken.IsBlocked;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task AreTokensBlockedAfterUserBlockingTestAsync()
        {
            var randomUser = await _context.Users.FirstAsync(x => x.Tokens.Any(t => !t.IsBlocked) && x.Role.RoleTitle != ApplicationRoles.Administrator);

            await _adminService.BlockUserAsync(randomUser.Id);

            var userTokens = _context.UsersTokens.Where(x => x.UserId == randomUser.Id);

            var actual = userTokens.Where(x => x.ExpirationDate >= DateTime.Now)
                                   .All(t => t.IsBlocked);

            var expected = true;

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public async Task AreTokensUnBlockedAfterUserUnBlockingTestAsync()
        {
            var randomUser = await _context.Users.FirstAsync(x =>
                                                                 x.IsBlocked
                                                                 && x.Tokens.Any(t => t.ExpirationDate > DateTime.Now)
                                                                 && x.Role.RoleTitle != ApplicationRoles.Administrator);

            await _adminService.UnBlockUserAsync(randomUser.Id);

            var userTokens = _context.UsersTokens.Where(x => x.UserId == randomUser.Id);

            var actual = userTokens.Where(x => x.ExpirationDate >= DateTime.Now)
                                   .All(t => !t.IsBlocked);

            var expected = true;

            Assert.AreEqual(actual, expected);
        }
    }
}