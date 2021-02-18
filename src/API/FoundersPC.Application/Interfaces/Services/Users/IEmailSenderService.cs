#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.UsersIdentity;

#endregion

namespace FoundersPC.Application.Interfaces.Services.Users
{
    public interface IEmailSenderService
    {
        Task<bool> SendConfirmationAsync(UserReadDto user);

        Task<int> SendManyAsync(IEnumerable<UserReadDto> users, string content);
    }
}