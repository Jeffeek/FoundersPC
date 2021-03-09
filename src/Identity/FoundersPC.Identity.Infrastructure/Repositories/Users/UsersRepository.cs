#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Repositories.Users;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.Contexts;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories.Users
{
    public class UsersRepository : GenericRepositoryAsync<UserEntity>, IUsersRepository
    {
        public UsersRepository(FoundersPCUsersContext context) : base(context) { }

        public override async Task<IEnumerable<UserEntity>> GetAllAsync() =>
            await Context.Set<UserEntity>()
                         .Include(user => user.Role)
                         .ToListAsync();

        public async Task<UserEntity> GetByAsync(Expression<Func<UserEntity, bool>> predicate) =>
            await Context.Set<UserEntity>()
                         .Include(user => user.Role)
                         .FirstOrDefaultAsync(predicate);

        public override async Task<UserEntity> GetByIdAsync(int id)
        {
            var user = await Context.Set<UserEntity>().FindAsync(id);

            if (user is null) return null;

            await Context.Entry(user).Reference(x => x.Role).LoadAsync();

            return user;
        }
    }
}