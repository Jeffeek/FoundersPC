﻿#region Using namespaces

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

        public async Task<UserEntity> GetUserByAsync(Expression<Func<UserEntity, bool>> predicate)
        {
            var user = await Context.Set<UserEntity>().FirstOrDefaultAsync(predicate);

            if (user is null) return null;

            await Context.Entry(user).Reference(x => x.Role).LoadAsync();
            await Context.Entry(user).Collection(x => x.Tokens).LoadAsync();
            await Context.Entry(user).Collection(x => x.Entrances).LoadAsync();

            return user;
        }

        public async Task<UserEntity> GetUserByEmailAsync(string userEmail)
        {
            var user = await Context.Set<UserEntity>().FirstOrDefaultAsync(x => x.Email == userEmail);

            if (user is null) return null;

            await Context.Entry(user).Reference(x => x.Role).LoadAsync();
            await Context.Entry(user).Collection(x => x.Tokens).LoadAsync();
            await Context.Entry(user).Collection(x => x.Entrances).LoadAsync();

            return user;
        }

        public override async Task<UserEntity> GetByIdAsync(int id)
        {
            var user = await Context.Set<UserEntity>().FindAsync(id);

            if (user is null) return null;

            await Context.Entry(user).Reference(x => x.Role).LoadAsync();
            await Context.Entry(user).Collection(x => x.Tokens).LoadAsync();
            await Context.Entry(user).Collection(x => x.Entrances).LoadAsync();

            return user;
        }
    }
}