using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Authentication
{
    public interface IIdentityUserInformationService
    {
        Task<string> GetUserLoginAsync(string email);

        Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokensAsync(string email);

        Task<NotificationsSettingsViewModel> GetUserNotificationsAsync(string email);
    }
}
