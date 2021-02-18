#region Using namespaces

using FoundersPC.Application.Interfaces.Repositories.Users;

#endregion

namespace FoundersPC.Infrastructure.Identity.UoW
{
    public interface IUnitOfWorkUsersIdentityAsync
    {
        IRolesRepositoryAsync RolesRepository { get; }

        IUsersRepositoryAsync UsersRepository { get; }
    }
}