#region Using namespaces

using FoundersPC.Application.Interfaces.Repositories.Users;

#endregion

namespace FoundersPC.Infrastructure.Identity.UoW
{
    public class UnitOfWorkUsersIdentityAsync : IUnitOfWorkUsersIdentityAsync
    {
        public UnitOfWorkUsersIdentityAsync(IRolesRepositoryAsync rolesRepository,
                                            IUsersRepositoryAsync usersRepository
        )
        {
            RolesRepository = rolesRepository;
            UsersRepository = usersRepository;
        }

        public IRolesRepositoryAsync RolesRepository { get; }

        public IUsersRepositoryAsync UsersRepository { get; }
    }
}