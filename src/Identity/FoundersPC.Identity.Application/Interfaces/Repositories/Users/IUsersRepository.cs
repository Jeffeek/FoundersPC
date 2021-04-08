#region Using namespaces

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Users
{
    public interface IUsersRepository : IRepositoryAsync<UserEntity>, IPaginateableRepository<UserEntity>
    {
        Task<UserEntity> GetUserByAsync(Expression<Func<UserEntity, bool>> predicate);

        Task<UserEntity> GetUserByEmailAsync(string userEmail);
    }
}