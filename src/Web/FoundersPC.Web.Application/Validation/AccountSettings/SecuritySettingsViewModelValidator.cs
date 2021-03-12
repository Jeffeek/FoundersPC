#region Using namespaces

using FluentValidation;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;

#endregion

namespace FoundersPC.Web.Application.Validation.AccountSettings
{
	public class SecuritySettingsViewModelValidator : AbstractValidator<SecuritySettingsViewModel>
	{
		public SecuritySettingsViewModelValidator()
		{
			RuleFor(model => model.NewLogin)
					.NotNull()
					.NotEmpty()
					.MinimumLength(5)
					.MaximumLength(30);
		}
	}
}