using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundersPC.Web.Models.ViewModels
{
    public class UserAccountSettingsViewModel
    {
        public UserPasswordViewModel PasswordViewModel { get; set; }

        public UserSecurityViewModel LoginViewModel { get; set; }

        public UserAccountInformationViewModel AccountInformationViewModel { get; set; }

        public UserNotificationsViewModel NotificationsViewModel { get; set; }

        public UserTokensViewModel TokensViewModel { get; set; }
    }
}
