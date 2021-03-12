using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.RequestResponseShared.Response.ChangeSettings;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;

namespace FoundersPC.Web.Application.Interfaces.Services.IdentityServer.User
{
    public interface IIdentityUserSettingsChangeService
	{
		Task<AccountSettingsChangeResponse> ChangePasswordAsync(PasswordSettingsViewModel model, string email, string token);

		Task<AccountSettingsChangeResponse> ChangeLoginAsync(SecuritySettingsViewModel model, string email, string token);

		Task<AccountSettingsChangeResponse> ChangeNotificationsAsync(NotificationsSettingsViewModel model, string email, string token);
	}
}
