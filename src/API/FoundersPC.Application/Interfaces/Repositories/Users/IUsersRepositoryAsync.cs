#region Using namespaces

using FoundersPC.Domain.Entities.Users;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Users
{
    public interface IUsersRepositoryAsync : IRepositoryAsync<User> { }
}