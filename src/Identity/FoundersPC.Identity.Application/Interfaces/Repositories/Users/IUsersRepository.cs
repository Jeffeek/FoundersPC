#region Using namespaces

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Domain.Entities;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Repositories
{
    public interface IUsersRepository : IRepositoryAsync<UserEntity>
    {
        Task<UserEntity> GetBy(Expression<Func<UserEntity, bool>> predicate);
    }
}