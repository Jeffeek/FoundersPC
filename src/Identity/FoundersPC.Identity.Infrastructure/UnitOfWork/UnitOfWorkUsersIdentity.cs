#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Repositories;
using FoundersPC.Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Identity.Infrastructure.UnitOfWork
{
    public class UnitOfWorkUsersIdentity : IUnitOfWorkUsersIdentity
    {
        private readonly FoundersPCUsersContext _context;

        public UnitOfWorkUsersIdentity(IUsersRepository usersRepository,
                                       IRolesRepository rolesRepository,
                                       FoundersPCUsersContext context
        )
        {
            _context = context;
            UsersRepository = usersRepository;
            RolesRepository = rolesRepository;
        }

        public IUsersRepository UsersRepository { get; }

        public IRolesRepository RolesRepository { get; }

        public async Task<int> SaveChangesAsync()
        {
            var saveChangesResult = 0;
            try
            {
                saveChangesResult = await _context.SaveChangesAsync();
            }
            catch
            {
                saveChangesResult = -1;
            }

            return saveChangesResult;
        }
    }
}