﻿#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Repositories.Logs;
using FoundersPC.Identity.Application.Interfaces.Repositories.Tokens;
using FoundersPC.Identity.Application.Interfaces.Repositories.Users;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.UnitOfWork
{
    public class UnitOfWorkUsersIdentity : IUnitOfWorkUsersIdentity
    {
        private readonly DbContext _context;

        public UnitOfWorkUsersIdentity(IUsersRepository usersRepository,
                                       IRolesRepository rolesRepository,
                                       DbContext context,
                                       IAccessTokensLogsRepository accessTokensLogsRepository,
                                       IUsersEntrancesLogsRepository usersEntrancesLogsRepository,
                                       IApiAccessUsersTokensRepository apiAccessUsersTokensRepository)
        {
            _context = context;
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
            }
            catch
            {
                saveChangesResult = -1;
            }

            return saveChangesResult;
        }
    }
}