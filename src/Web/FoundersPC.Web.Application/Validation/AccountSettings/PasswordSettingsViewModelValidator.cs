#region Using namespaces

using FluentValidation;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;

#endregion

namespace FoundersPC.Web.Application.Validation.AccountSettings
{
	public class PasswordSettingsViewModelValidator : AbstractValidator<PasswordSettingsViewModel>
	{
		public PasswordSettingsViewModelValidator()
		{
			RuleFor(model => model.NewPassword)
					.NotNull()
					.NotEmpty()
					.MinimumLength(6)
					.MaximumLength(30)
					.Equal(model => model.NewPasswordConfirm);

			RuleFor(model => model.NewPasswordConfirm)
					.NotNull()
					.NotEmpty()
					.MinimumLength(6)
					.MaximumLength(30)
					.Equal(model => model.NewPassword);

			RuleFor(model => model.OldPassword)
					.NotNull()
					.NotEmpty()
					.MinimumLength(6)
					.MaximumLength(30);
		}
	}
}