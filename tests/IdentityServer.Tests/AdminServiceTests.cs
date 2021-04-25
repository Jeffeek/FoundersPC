#region Using namespaces

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
using IdentityServer.Tests.MockDb;
using IdentityServer.Tests.MockDb.DataCreation;
using IdentityServer.Tests.MockDb.MockServices;
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
            _context = DB.GetInMemoryContext();

            var mapper = new MapperConfiguration(x => x.AddProfile(typeof(MappingStartup)));

            _unitOfWork = new UnitOfWorkUsersIdentity(new UsersRepository(_context),
                                                      new RolesRepository(_context),
                                                      new AccessTokensLogsRepository(_context),
                                                      new UsersEntrancesLogsRepository(_context),
                                                      new AccessTokensRepository(_context),
                                                      _context,
                                                      new NullLogger<UnitOfWorkUsersIdentity>());

            _adminService = new AdminService(new MockEmailService(),
                                             _unitOfWork,
                                             new AccessUsersTokensService(_unitOfWork,
                                                                          new AccessTokenReservationService(_unitOfWork,
                                                                              new TokenEncryptorService(),
                                                                              new MockEmailService(),
                                                                              new NullLogger<AccessTokenReservationService>()),
                                                                          new AccessTokensBlockingService(new NullLogger<AccessTokensBlockingService>(),
                                                                                                              _unitOfWork),
                                                                          new AccessTokensRequestsService(new NullLogger<AccessTokensRequestsService>(),
                                                                                                              _unitOfWork),
                                                                          new AccessTokensStatusService(_unitOfWork,
                                                                                                        new NullLogger<AccessTokensStatusService>()),
                                                                          new Mapper(mapper),
                                                                          new NullLogger<AccessUsersTokensService>()),
                                             new NullLogger<AdminService>());

            var roles = IdentityServerDataCreation.GenerateRoles();

            await _context.Roles.AddRangeAsync(roles);

            var users = IdentityServerDataCreation.UsersFaker.Generate(200);

            await _context.Users.AddRangeAsync(users);

            var entrances = IdentityServerDataCreation.UsersEntrancesFaker.Generate(3000);

            await _context.UsersEntrancesLogs.AddRangeAsync(entrances);

            var accessTokensLogs = IdentityServerDataCreation.AccessTokenLogsFaker.Generate(500);

            await _context.TokenAccessLogs.AddRangeAsync(accessTokensLogs);

            await _context.SaveChangesAsync();
        }

        private IUnitOfWorkUsersIdentity _unitOfWork;

        private FoundersPCUsersContext _context;

        private IAdminService _adminService;

        [Test]
        public async Task BlockUserTest()
        {
            var firstUnblockedUser = await _context.Users.FirstOrDefaultAsync(x => !x.IsBlocked
                                                                                   && x.Role.RoleTitle != ApplicationRoles.Administrator);

            await _adminService.BlockUserAsync(firstUnblockedUser.Id);

            var actual = firstUnblockedUser.IsBlocked;

            var expected = true;

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public async Task TryToBlockAdministrator()
        {
            var firstAdmin = await _context.Users.FirstOrDefaultAsync(x => x.Role.RoleTitle == ApplicationRoles.Administrator);

            await _adminService.BlockUserAsync(firstAdmin.Id);

            var actual = firstAdmin.IsBlocked;

            var expected = false;

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public async Task BlockTokenTest()
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
    }
}