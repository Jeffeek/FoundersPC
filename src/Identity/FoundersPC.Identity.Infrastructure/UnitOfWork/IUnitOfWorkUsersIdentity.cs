#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Repositories;

#endregion

namespace FoundersPC.Identity.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkUsersIdentity
    {
        IUsersRepository UsersRepository { get; }

        IRolesRepository RolesRepository { get; }

        Task<int> SaveChangesAsync();
    }
}