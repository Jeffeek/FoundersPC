#region Using namespaces

using System;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Repositories.Logs;
using FoundersPC.Identity.Application.Interfaces.Repositories.Tokens;
using FoundersPC.Identity.Application.Interfaces.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Infrastructure.UnitOfWork
{
    public class UnitOfWorkUsersIdentity : IUnitOfWorkUsersIdentity
    {
        private readonly DbContext _context;
        private readonly ILogger<UnitOfWorkUsersIdentity> _logger;

        public UnitOfWorkUsersIdentity(IUsersRepository usersRepository,
                                       IRolesRepository rolesRepository,
                                       DbContext context,
                                       IAccessTokensLogsRepository accessTokensLogsRepository,
                                       IUsersEntrancesLogsRepository usersEntrancesLogsRepository,
                                       IApiAccessUsersTokensRepository apiAccessUsersTokensRepository,
                                       ILogger<UnitOfWorkUsersIdentity> logger)
        {
            _context = context;
            _logger = logger;
            AccessTokensLogsRepository = accessTokensLogsRepository;
            UsersEntrancesLogsRepository = usersEntrancesLogsRepository;
            ApiAccessUsersTokensRepository = apiAccessUsersTokensRepository;
            UsersRepository = usersRepository;
            RolesRepository = rolesRepository;
        }

        public IAccessTokensLogsRepository AccessTokensLogsRepository { get; }

        public IUsersEntrancesLogsRepository UsersEntrancesLogsRepository { get; }

        public IApiAccessUsersTokensRepository ApiAccessUsersTokensRepository { get; }

        public IUsersRepository UsersRepository { get; }

        public IRolesRepository RolesRepository { get; }

        public async Task<int> SaveChangesAsync()
        {
            int saveChangesResult;

            try
            {
                saveChangesResult = await _context.SaveChangesAsync();
                _logger.LogInformation($"Successful save changes in {nameof(UnitOfWorkUsersIdentity)}");
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                                 $"Error occured when tried to make save changes in {nameof(UnitOfWorkUsersIdentity)}");

                saveChangesResult = -1;
            }

            return saveChangesResult;
        }
    }
}