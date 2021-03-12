﻿namespace FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings
{
	public class AccountSettingsViewModel
	{
		public PasswordSettingsViewModel PasswordSettingsViewModel { get; set; }

		public SecuritySettingsViewModel LoginSettingsViewModel { get; set; }

		public AccountInformationViewModel AccountInformationViewModel { get; set; }

		public NotificationsSettingsViewModel NotificationsSettingsViewModel { get; set; }

		public UserTokensViewModel TokensViewModel { get; set; }
	}
}