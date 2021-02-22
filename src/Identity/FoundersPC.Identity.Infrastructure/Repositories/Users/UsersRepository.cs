#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Application.Interfaces.Repositories;
using FoundersPC.Identity.Domain.Entities;
using FoundersPC.Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories
{
    public class UsersRepository : GenericRepositoryAsync<UserEntity>, IUsersRepository
    {
        public UsersRepository(FoundersPCUsersContext context) : base(context) { }

        public async Task<IEnumerable<UserEntity>> GetAllAsync() =>
            await Context.Set<UserEntity>()
                         .Include(user => user.Role)
                         .ToListAsync();

        public async Task<UserEntity> GetBy(Expression<Func<UserEntity, bool>> predicate)
        {
            await Context.Set<UserEntity>().LoadAsync();
            var user = await Context.Set<UserEntity>().FirstOrDefaultAsync(predicate);

            return user;
        }
    }
}