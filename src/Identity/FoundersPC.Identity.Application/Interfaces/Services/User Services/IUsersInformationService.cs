#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.User_Services
{
    public interface IUsersInformationService : IPaginateableService<UserEntityReadDto>
    {
        Task<IEnumerable<UserEntityReadDto>> GetAllUsersAsync();

        Task<IEnumerable<UserEntityReadDto>> GetAllNotBlockedUsersAsync();

        Task<IEnumerable<UserEntityReadDto>> GetAllActiveUsersAsync();

        Task<UserEntityReadDto> GetUserByIdAsync(int id);

        Task<UserEntityReadDto> FindUserByEmailAsync(string email);
    }
}