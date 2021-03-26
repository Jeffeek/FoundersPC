#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.WebIdentityShared;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User
{
    public interface IUserSettingsWebService
    {
        Task<ApplicationUser> GetOverallInformation(string email, string token);
    }
}