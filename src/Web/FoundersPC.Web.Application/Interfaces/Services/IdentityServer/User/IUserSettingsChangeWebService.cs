#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.RequestResponseShared.Response.ChangeSettings;
using FoundersPC.Web.Domain.Common.AccountSettings;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User
{
    public interface IUserSettingsChangeWebService
    {
        Task<AccountSettingsChangeResponse> ChangePasswordAsync(PasswordSettingsViewModel model, string token);

        Task<AccountSettingsChangeResponse> ChangeLoginAsync(SecuritySettingsViewModel model, string token);

        Task<AccountSettingsChangeResponse>
            ChangeNotificationsAsync(NotificationsSettingsViewModel model, string token);
    }
}