#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User
{
    public interface IUserSettingsWebService
    {
        Task<UserEntityReadDto> GetOverallInformation(string email, string token);
    }
}