#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Repositories.Users;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.Repositories.Users
{
    public class RolesRepository : GenericRepositoryAsync<RoleEntity>, IRolesRepository
    {
        public RolesRepository(DbContext context) : base(context) { }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="navigationPropertyPath"/>
        ///     is <see langword="null"/>.
        /// </exception>

        #endregion

        public override async Task<IEnumerable<RoleEntity>> GetAllAsync() =>
            await Context.Set<RoleEntity>()
                         .Include(role => role.Users)
                         .ToListAsync();
    }
}