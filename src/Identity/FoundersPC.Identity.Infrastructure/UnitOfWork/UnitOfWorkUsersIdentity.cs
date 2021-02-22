#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Repositories.Logs;
using FoundersPC.Identity.Application.Interfaces.Repositories.Tokens;
using FoundersPC.Identity.Application.Interfaces.Repositories.Users;
using FoundersPC.Identity.Infrastructure.Contexts;

#endregion

namespace FoundersPC.Identity.Infrastructure.UnitOfWork
{
    public class UnitOfWorkUsersIdentity : IUnitOfWorkUsersIdentity
    {
        private readonly FoundersPCUsersContext _context;

        public UnitOfWorkUsersIdentity(IUsersRepository usersRepository,
                                       IRolesRepository rolesRepository,
                                       FoundersPCUsersContext context,
                                       IAccessTokensLogsRepository accessTokensLogsRepository,
                                       IUsersEntrancesLogsRepository usersEntrancesLogsRepository,
                                       IApiAccessTokensRepository apiAccessTokensRepository,
                                       IApiAccessUsersTokensRepository apiAccessUsersTokensRepository
        )
        {
            _context = context;
            AccessTokensLogsRepository = accessTokensLogsRepository;
            UsersEntrancesLogsRepository = usersEntrancesLogsRepository;
            ApiAccessTokensRepository = apiAccessTokensRepository;
            ApiAccessUsersTokensRepository = apiAccessUsersTokensRepository;
            UsersRepository = usersRepository;
            RolesRepository = rolesRepository;
        }

        public IAccessTokensLogsRepository AccessTokensLogsRepository { get; }

        public IUsersEntrancesLogsRepository UsersEntrancesLogsRepository { get; }

        public IApiAccessTokensRepository ApiAccessTokensRepository { get; }

        public IApiAccessUsersTokensRepository ApiAccessUsersTokensRepository { get; }

        public IUsersRepository UsersRepository { get; }

        public IRolesRepository RolesRepository { get; }

        public async Task<int> SaveChangesAsync()
        {
            int saveChangesResult;
            try
            {
                saveChangesResult = await _context.SaveChangesAsync();
            }
            catch
            {
                saveChangesResult = -1;
            }

            return saveChangesResult;
        }
    }
}