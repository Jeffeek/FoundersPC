#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
    public interface IAuthenticationService
    {
        Task<UserEntityReadDto> FindUserByEmailOrLoginAndHashedPasswordAsync(string emailOrLogin, string hashedPassword);

        Task<UserEntityReadDto> FindUserByEmailOrLoginAndPasswordAsync(string emailOrLogin, string password);
    }
}