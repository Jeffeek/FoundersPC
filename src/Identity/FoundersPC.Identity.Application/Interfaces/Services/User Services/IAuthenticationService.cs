using System.Threading.Tasks;
using FoundersPC.Identity.Application.DTO;

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
    public interface IAuthenticationService
    {
        Task<UserEntityReadDto> FindUserByEmailOrLoginAndHashedPasswordAsync(string emailOrLogin, string hashedPassword);

        Task<UserEntityReadDto> FindUserByEmailOrLoginAndPasswordAsync(string emailOrLogin, string password);
    }
}
