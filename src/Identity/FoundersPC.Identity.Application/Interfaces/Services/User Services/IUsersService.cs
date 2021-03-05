using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.DTO;

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserEntityReadDto>> GetAllAsync();

        Task<UserEntityReadDto> GetByIdAsync(int id);

        Task<UserEntityReadDto> FindUserByEmailAsync(string email);

        Task<UserEntityReadDto> FindUserByEmailOrLoginAndHashedPasswordAsync(string emailOrLogin, string hashedPassword);

        Task<UserEntityReadDto> FindUserByEmailOrLoginAndPasswordAsync(string emailOrLogin, string password);

        Task<bool> ChangePasswordToAsync(int userId, string newPassword);

        Task<bool> ChangePasswordToAsync(string userEmail, string newPassword);

        Task<bool> ChangePasswordToAsync(UserEntityReadDto user, string newPassword);
    }
}
