#region Using namespaces

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Infrastructure.Contexts;
using FoundersPC.Identity.Infrastructure.Repositories.Logs;
using FoundersPC.Identity.Infrastructure.Repositories.Tokens;
using FoundersPC.Identity.Infrastructure.Repositories.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using IdentityServer.Tests.MockDb;
using IdentityServer.Tests.MockDb.DataCreation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

#endregion

namespace IdentityServer.Tests
{
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    [TestFixture]
    public class AdminServiceTests
    {
        [OneTimeSetUp]
        public async Task SetUpAsync()
        {
            _context = DB.GetInMemoryContext();

            _unitOfWork = new UnitOfWorkUsersIdentity(new UsersRepository(_context),
                                                      new RolesRepository(_context),
                                                      new AccessTokensLogsRepository(_context),
                                                      new UsersEntrancesLogsRepository(_context),
                                                      new AccessTokensRepository(_context),
                                                      _context,
                                                      new NullLogger<UnitOfWorkUsersIdentity>());

            var roles = HC.GenerateRoles();

            await _context.Roles.AddRangeAsync(roles);

            var users = HC.UsersFaker.Generate(200);

            await _context.Users.AddRangeAsync(users);

            var entrances = HC.UsersEntrancesFaker.Generate(3000);

            await _context.UsersEntrancesLogs.AddRangeAsync(entrances);

            await _context.SaveChangesAsync();
        }

        private IUnitOfWorkUsersIdentity _unitOfWork;

        private FoundersPCUsersContext _context;

        [Test]
        public async Task CountOfAdminsAsync()
        {
            var adminRole = await _context.Roles.FirstOrDefaultAsync(x => x.RoleTitle == ApplicationRoles.Administrator);
            adminRole.Users.Count.ToString();
        }
    }
}