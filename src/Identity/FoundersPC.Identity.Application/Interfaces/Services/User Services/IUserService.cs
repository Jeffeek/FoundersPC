#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Application.DTO;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
    public interface IUserService
    {
        Task<UserEntityReadDto> GetUserWithEmailAndPasswordAsync(string emailOrLogin, string rawPassword);

        Task<UserEntityReadDto> GetUserWithEmailAsync(string email);

        Task<bool> RegisterUserAsync(string email, string rawPassword);

        Task<bool> RegisterManagerAsync(string email, string rawPassword);

        Task<bool> ChangePassword(int id, string newPassword);
    }
}