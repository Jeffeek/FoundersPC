#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication
{
    public interface IIdentityUserInformationService
    {
        Task<string> GetUserLoginAsync(string login, string token);

        Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokensAsync(string email, string token);

        Task<NotificationsSettingsViewModel> GetUserNotificationsAsync(string email, string token);
    }
}