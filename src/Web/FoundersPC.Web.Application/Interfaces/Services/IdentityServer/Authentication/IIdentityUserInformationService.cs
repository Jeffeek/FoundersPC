#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;
using FoundersPC.WebIdentityShared;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication
{
    public interface IIdentityUserInformationService
    {
        Task<string> GetUserLoginAsync(string login, string token);

        Task<IEnumerable<ApplicationAccessToken>> GetUserTokensAsync(string email, string token);

        Task<NotificationsSettingsViewModel> GetUserNotificationsAsync(string email, string token);

        Task<ApplicationUser> GetOverallInformation(string email, string token);
    }
}