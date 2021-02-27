#region Using namespaces

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Domain.Entities.Users;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories.Users
{
    public interface IUsersRepository : IRepositoryAsync<UserEntity>
    {
        Task<UserEntity> GetByAsync(Expression<Func<UserEntity, bool>> predicate);
    }
}