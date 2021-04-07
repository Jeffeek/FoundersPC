﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Repositories.Logs;
using FoundersPC.Identity.Domain.Entities.Logs;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.Contexts;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories.Logs
{
    public class UsersEntrancesLogsRepository : GenericRepositoryAsync<UserEntranceLog>, IUsersEntrancesLogsRepository
    {
        public UsersEntrancesLogsRepository(FoundersPCUsersContext context) : base(context) { }

        public override async Task<IEnumerable<UserEntranceLog>> GetAllAsync() =>
            await Context.Set<UserEntranceLog>()
                         .Include(log => log.User)
                         .ThenInclude(user => user.Role)
                         .ToListAsync();

        public override async Task<UserEntranceLog> GetByIdAsync(int id)
        {
            var entrance = await Context.Set<UserEntranceLog>()
                                        .FindAsync(id);

            if (entrance is null) return null;

            Context.Entry(entrance)
                   .Reference(x => x.User);

            Context.Entry(entrance)
                   .Reference(x => x.User.Role);

            return entrance;
        }

        public async Task<IEnumerable<UserEntranceLog>> GetEntrancesBetweenAsync(DateTime start, DateTime finish) =>
            await Context.Set<UserEntranceLog>()
                         .Where(log => log.Entrance >= start && log.Entrance <= finish)
                         .ToListAsync();

        public async Task<IEnumerable<UserEntranceLog>> GetEntrancesInAsync(DateTime date) =>
            await Context.Set<UserEntranceLog>()
                         .Where(log => log.Entrance.Year == date.Year
                                       && log.Entrance.Month == date.Month
                                       && log.Entrance.Day == date.Day)
                         .ToListAsync();

        public async Task<IEnumerable<UserEntranceLog>> GetUserEntrancesAsync(int userId)
        {
            var user = await Context.Set<UserEntity>()
                                    .FindAsync(userId);

            if (user is null) return null;

            await Context.Entry(user)
                         .Collection(x => x.Entrances)
                         .LoadAsync();

            return user.Entrances;
        }

        public async Task<IEnumerable<UserEntranceLog>> GetUserEntrancesAsync(string userEmail)
        {
            var user = await Context.Set<UserEntity>()
                                    .SingleOrDefaultAsync(x => x.Email == userEmail);

            if (user is null) return null;

            await Context.Entry(user)
                         .Collection(x => x.Entrances)
                         .LoadAsync();

            return user.Entrances;
        }
    }
}